using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Contract.DomainModels.UpdateModels;
using ShoppingCart.Core.Entity;
using ShoppingCart.Global.Enums;

namespace ShoppingCart.Core.Interfaces;

public interface IProduct
{
    ProductType ProductType { get; init; }
    public event EventHandler PriceUpdated;
    public event Action<object,  CanAddContainer> BeforeAddingVasProduct;
}