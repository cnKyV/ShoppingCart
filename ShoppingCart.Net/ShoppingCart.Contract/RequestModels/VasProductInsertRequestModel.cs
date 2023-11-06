using Newtonsoft.Json;
using ShoppingCart.Contract.Interfaces;

namespace ShoppingCart.Contract.RequestModels;

public class VasProductInsertRequestModel : Payload
{
    [JsonProperty("vasItemId")]
    public int VasItemId { get; set; }
    [JsonProperty("vasCategoryId")]
    public int VasCategoryId { get; set; }
    [JsonProperty("vasSellerId")]
    public int VasSellerId { get; set; }
    [JsonProperty("price")]
    public decimal Price { get; set; }
    [JsonProperty("quantity")]
    public int Quantity { get; set; }
}