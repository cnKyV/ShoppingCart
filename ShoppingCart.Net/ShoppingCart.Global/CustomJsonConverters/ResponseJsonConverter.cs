using Newtonsoft.Json;

namespace ShoppingCart.Global.CustomJsonConverters;

public class ResponseJsonConverter : JsonConverter<string>
{
    public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
    {
        writer.WriteRawValue(value);
    }

    public override bool CanRead => false;
    public override bool CanWrite => true;
}