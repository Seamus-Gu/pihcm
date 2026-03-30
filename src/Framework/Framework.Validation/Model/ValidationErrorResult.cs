using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Framework.Validation
{
    /// <summary>
    /// 校验失败时返回的统一结果。
    /// </summary>
    internal sealed class ValidationErrorResult : ObjectResult
    {
        public ValidationErrorResult(IReadOnlyList<ValidationFailure> errors)
            : base(CreateResponse(errors))
        {
            this.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        }

        /// <summary>
        /// 创建统一响应对象。
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static ValidationResult CreateResponse(IReadOnlyList<ValidationFailure> errors)
        {
            var httpCode = (int)HttpStatusCode.UnprocessableEntity;
            var data = errors.ToList();

            if (errors.Count == 1)
            {
                var first = errors[0];
                return new ValidationResult
                {
                    Code = first.Code,
                    Message = first.Message,
                    Data = data
                };
            }

            return new ValidationResult
            {
                Code = httpCode,
                Message = errors.Count > 1 ? "存在多个问题" : "参数校验失败",
                Data = data
            };
        }
    }
}