using Newtonsoft.Json;

namespace ShoppingCart.Contract.RequestModels;

public class Payload
{
    [JsonProperty("itemId")]
    public int ItemId { get; set; }
}