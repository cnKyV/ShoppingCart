using System.Text.Json.Serialization;
using ShoppingCart.Contract.RequestModels;

namespace ShoppingCart.Contract.Interfaces;

public class CommandInput
{
    [JsonPropertyName("command")]
    public string Command { get; set; }
    [JsonPropertyName("payload")]
    public Payload? Payload { get; set; }
}