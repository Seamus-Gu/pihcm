using System.Text.Json;
using System.Text.Json.Serialization;

namespace Framework.Core
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            if (dateString is null)
                throw new ArgumentException(nameof(reader));
            return DateTime.Parse(dateString);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateTimeConstant.DATE_LONG));
        }
    }
}