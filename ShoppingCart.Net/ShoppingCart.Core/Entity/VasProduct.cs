using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Contract.DomainModels.UpdateModels;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Global.Enums;

namespace ShoppingCart.Core.Entity;

public partial class VasProduct : BaseEntity<VasProduct>, IProduct
{
    public VasProduct(VasProductCreateModel vasProductCreateModel)
    {
        ItemId = vasProductCreateModel.ItemId;
        AppliedItemId = vasProductCreateModel.AppliedProductId;
        CategoryId = vasProductCreateModel.CategoryId;
        SellerId = vasProductCreateModel.SellerId;
        Price = vasProductCreateModel.Price;
        Quantity = vasProductCreateModel.Quantity;
        ProductType = vasProductCreateModel.ProductType;
    }

    public VasProduct()
    {
        
    }

    public event EventHandler? PriceUpdated;
    public event Action<object, CanAddContainer>? BeforeAddingVasProduct;
}