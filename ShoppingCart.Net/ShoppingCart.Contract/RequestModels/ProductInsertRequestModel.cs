using Newtonsoft.Json;
using ShoppingCart.Contract.Interfaces;

namespace ShoppingCart.Contract.RequestModels;

public class ProductInsertRequestModel : Payload
{
    [JsonProperty("categoryId")]
    public int CategoryId { get; set; }
    [JsonProperty("sellerId")]
    public int SellerId { get; set; }
    [JsonProperty("price")]
    public decimal Price { get; set; }
    [JsonProperty("quantity")]
    public int Quantity { get; set; }
}