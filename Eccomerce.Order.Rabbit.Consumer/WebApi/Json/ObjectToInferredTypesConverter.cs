using System.Text.Json.Serialization;
using System.Text.Json;

namespace Eccomerce.Order.Rabbit.Consumer.WebApi.Json
{
    public class ObjectToInferredTypesConverter : JsonConverter<object>
    {
        public override object Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.True)
                return true;
            else if (reader.TokenType == JsonTokenType.False)
                return false;
            else if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt64(out long l))
                    return l;
                return reader.GetDouble();
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                if (reader.TryGetDateTime(out DateTime datetime))
                    return datetime;
                return reader.GetString();
            }

            return JsonDocument.ParseValue(ref reader).RootElement.Clone();
            // JsonTokenType.String => reader.GetString()!, => JsonDocument.ParseValue(ref reader).RootElement.Clone()
        }

        public override void Write(
            Utf8JsonWriter writer,
            object objectToWrite,
            JsonSerializerOptions options) =>
            JsonSerializer.Serialize(writer, objectToWrite, objectToWrite.GetType(), options);
    }
}