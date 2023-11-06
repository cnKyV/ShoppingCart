using ShoppingCart.Global.Enums;

namespace ShoppingCart.Contract.DomainModels.CreateModels;

public record DigitalProductCreateModel : ProductCreateModel
{
    public Uri DownloadUrl { get; set; }
}