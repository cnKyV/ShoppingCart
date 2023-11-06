using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Contract.DomainModels.UpdateModels;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.ValueObjects;
using ShoppingCart.Global.Enums;
using ShoppingCart.Global.Utility.Settings;
using ShoppingCart.Global.Utility;

namespace ShoppingCart.Core.Entity;

public partial class Product : BaseEntity<Product>, IProduct
{
    public Product()
    {
        
    }
    public Product(ProductCreateModel productCreateModel)
    {
        ItemId = productCreateModel.ItemId;
        CategoryId = productCreateModel.CategoryId;
        SellerId = productCreateModel.SellerId;
        Price.TotalPrice = productCreateModel.Price;
        Quantity = productCreateModel.Quantity;
        ProductType = productCreateModel.ProductType;
    }
    public int ItemId { get; private set; }
    public int CategoryId { get; private set; }
    public int SellerId { get; private set; }
    public Price Price { get; private set; } = new();
    public ProductType ProductType { get; init; }
    public int Quantity { get; private set; }
    private bool VasProductMaximumNumberOfPossibleElementsExceeded => VasProducts.Count >=
                                                                      _productSettings.VasProductDefaults
                                                                          .MaximumNumberOfVasProductsCanBeAppliedToProduct;
    private static readonly VasProductDefaults VasProductDefaults = ConfigurationManager.ApplicationSettings.ProductSettings.VasProductDefaults;
    public event EventHandler? PriceUpdated;
    public event Action<object, CanAddContainer> BeforeAddingVasProduct;
}