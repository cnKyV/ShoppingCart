using ShoppingCart.Contract.DomainModels.CreateModels;

namespace ShoppingCart.Core.Entity;

public partial class DigitalProduct
{
    public DigitalProduct(DigitalProductCreateModel digitalProductCreate) : base(digitalProductCreate)
    {
        DownloadLink = digitalProductCreate.DownloadUrl;
    }
    public Uri? DownloadLink { get; private set; }
}