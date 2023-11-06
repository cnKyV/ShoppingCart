using ShoppingCart.Global.Enums;

namespace ShoppingCart.Contract.DomainModels.UpdateModels;

public record DigitalProductUpdateModel : ProductUpdateModel
{
    public Uri DownloadUrl { get; set; }
}