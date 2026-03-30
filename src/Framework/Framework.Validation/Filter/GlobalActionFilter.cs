using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;

namespace Framework.Validation
{
    /// <summary>
    /// 请求参数校验筛选器，用于统一处理模型绑定后的校验错误。
    /// </summary>
    public class ValidationActionFilter : ActionFilterAttribute
    {
        private const char PATH_SEGMENT_SEPARATOR = '.';
        private const char INDEXER_START_TOKEN = '[';
        private static readonly BindingFlags _propertyLookupFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;

        /// <summary>
        /// 在 Action 执行前拦截模型校验结果，并返回统一的校验响应。
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var candidates = GetCandidateArguments(context).ToArray();
            var errorCodeCache = new Dictionary<string, ValidationErrorEnum?>(StringComparer.OrdinalIgnoreCase);
            var errors = new List<ValidationFailure>();

            foreach (var modelStateEntry in context.ModelState)
            {
                if (modelStateEntry.Value?.Errors.Count is not > 0)
                {
                    continue;
                }

                foreach (var error in modelStateEntry.Value.Errors)
                {
                    errors.Add(BuildValidationFailure(modelStateEntry.Key, error.ErrorMessage, candidates, errorCodeCache));
                }
            }

            context.Result = new ValidationErrorResult(errors);
        }

        /// <summary>
        /// 构建校验失败明细。
        /// </summary>
        /// <param name="field"></param>
        /// <param name="message"></param>
        /// <param name="candidates"></param>
        /// <param name="errorCodeCache"></param>
        /// <returns></returns>
        private static ValidationFailure BuildValidationFailure(
            string field,
            string message,
            IReadOnlyList<(string Name, Type? Type)> candidates,
            IDictionary<string, ValidationErrorEnum?> errorCodeCache)
        {
            var errorEnum = ResolveErrorCode(field, candidates, errorCodeCache);
            var localizedMessage = errorEnum is { } code
                ? ValidationMessages.GetString(code.GetName())
                : message;
            var finalMessage = FormatMessage(localizedMessage, field, message);

            return new ValidationFailure
            {
                Field = field,
                Message = finalMessage,
                Code = errorEnum?.ToInt() ?? (int)HttpStatusCode.UnprocessableEntity
            };
        }

        /// <summary>
        /// 解析字段对应的业务校验错误码。
        /// </summary>
        /// <param name="field"></param>
        /// <param name="candidates"></param>
        /// <param name="errorCodeCache"></param>
        /// <returns></returns>
        private static ValidationErrorEnum? ResolveErrorCode(
            string field,
            IReadOnlyList<(string Name, Type? Type)> candidates,
            IDictionary<string, ValidationErrorEnum?> errorCodeCache)
        {
            if (errorCodeCache.TryGetValue(field, out var cached))
            {
                return cached;
            }

            ValidationErrorEnum? resolved = null;

            foreach (var (name, type) in candidates)
            {
                if (TryGetErrorCode(name, type, field, out var errorCode))
                {
                    resolved = errorCode;
                    break;
                }
            }

            errorCodeCache[field] = resolved;
            return resolved;
        }

        /// <summary>
        /// 获取参与错误码匹配的参数集合。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static IEnumerable<(string Name, Type? Type)> GetCandidateArguments(ActionExecutingContext context)
        {
            foreach (var actionArgument in context.ActionArguments)
            {
                yield return (actionArgument.Key, actionArgument.Value?.GetType());
            }

            if (context.ActionArguments.Count == 1)
            {
                var actionArgument = context.ActionArguments.First();
                yield return (string.Empty, actionArgument.Value?.GetType());
            }
        }

        /// <summary>
        /// 尝试获取字段对应的错误码。
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterType"></param>
        /// <param name="field"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private static bool TryGetErrorCode(string parameterName, Type? parameterType, string field, out ValidationErrorEnum code)
        {
            code = default;

            if (parameterType is null || string.IsNullOrWhiteSpace(field))
            {
                return false;
            }

            var propertyPath = NormalizePropertyPath(parameterName, field);
            if (propertyPath is null)
            {
                return false;
            }

            var property = FindProperty(parameterType, propertyPath);
            if (property is null)
            {
                return false;
            }

            var provider = GetValidationCodeProvider(property);
            if (provider is null)
            {
                return false;
            }

            code = provider.Error;
            return true;
        }

        /// <summary>
        /// 将 ModelState 字段路径转换为属性路径。
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        private static string? NormalizePropertyPath(string parameterName, string field)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                return field;
            }

            if (field.Equals(parameterName, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            if (field.StartsWith(parameterName + ".", StringComparison.OrdinalIgnoreCase))
            {
                return field[(parameterName.Length + 1)..];
            }

            if (field.StartsWith(parameterName + "[", StringComparison.OrdinalIgnoreCase))
            {
                return field[parameterName.Length..];
            }

            return null;
        }

        /// <summary>
        /// 按属性路径查找目标属性。
        /// </summary>
        /// <param name="rootType"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static PropertyInfo? FindProperty(Type rootType, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            var currentType = rootType;
            PropertyInfo? property = null;
            var remaining = path.AsSpan();

            while (!remaining.IsEmpty)
            {
                var separatorIndex = remaining.IndexOf(PATH_SEGMENT_SEPARATOR);
                var segment = separatorIndex >= 0 ? remaining[..separatorIndex] : remaining;
                remaining = separatorIndex >= 0 ? remaining[(separatorIndex + 1)..] : ReadOnlySpan<char>.Empty;

                if (segment.IsEmpty)
                {
                    continue;
                }

                var propertyName = ExtractPropertyName(segment);
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    currentType = ResolveElementType(currentType) ?? currentType;
                    continue;
                }

                property = currentType.GetProperty(propertyName, _propertyLookupFlags);
                if (property is null)
                {
                    return null;
                }

                currentType = property.PropertyType;
                if (segment.Contains(INDEXER_START_TOKEN))
                {
                    currentType = ResolveElementType(currentType) ?? currentType;
                }
            }

            return property;
        }

        /// <summary>
        /// 获取属性上的错误码提供者，优先返回非 ValidationAttribute 的实现。
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private static IValidationCodeProvider? GetValidationCodeProvider(PropertyInfo property)
        {
            var providers = property.GetCustomAttributes(true).OfType<IValidationCodeProvider>().ToArray();
            return providers.FirstOrDefault(attribute => attribute is not ValidationAttribute)
                ?? providers.FirstOrDefault();
        }

        /// <summary>
        /// 提取路径段中的属性名。
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        private static string ExtractPropertyName(ReadOnlySpan<char> segment)
        {
            var index = segment.IndexOf(INDEXER_START_TOKEN);
            return index >= 0 ? segment[..index].ToString() : segment.ToString();
        }

        /// <summary>
        /// 解析集合或数组的元素类型。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Type? ResolveElementType(Type type)
        {
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            if (!type.IsGenericType)
            {
                return null;
            }

            return type.GetGenericArguments().FirstOrDefault();
        }

        /// <summary>
        /// 格式化错误消息，优先使用本地化消息。
        /// </summary>
        /// <param name="localizedMessage"></param>
        /// <param name="field"></param>
        /// <param name="fallbackMessage"></param>
        /// <returns></returns>
        private static string FormatMessage(string? localizedMessage, string field, string fallbackMessage)
        {
            if (string.IsNullOrWhiteSpace(localizedMessage))
            {
                return fallbackMessage;
            }

            return localizedMessage.Contains("{0}", StringComparison.Ordinal)
                ? string.Format(localizedMessage, field)
                : localizedMessage;
        }
    }
}