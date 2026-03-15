using System.ComponentModel;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Framework.Core
{
    public class JsonUtil
    {
        public static T? Deseriallize<T>(string body) where T : class
        {
            JsonSerializerOptions options = new JsonSerializerOptions();

            options.Converters.Add(new BooleanConverter()); // 布尔处理
            options.Converters.Add(new DateTimeConverter());//时间处理
            options.Converters.Add(new DateTimeNullableConverter());//时间处理
            options.Converters.Add(new LongConverter());//时间处理
            options.ReferenceHandler = ReferenceHandler.IgnoreCycles;//忽略循环引用
            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;//处理乱码问题
            options.IncludeFields = true;//包含成员字段序列化
            options.AllowTrailingCommas = true;//允许尾随逗号
            options.WriteIndented = true;//是否应使用整齐打印
            options.ReadCommentHandling = JsonCommentHandling.Skip;//允许注释
            options.PropertyNameCaseInsensitive = true;//不区分大小写
            options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;//驼峰
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;//驼峰

            return JsonSerializer.Deserialize<T>(body, options);
        }

        public static string Serialize(object value)
        {
            return JsonSerializer.Serialize(value);
        }
    }
}
