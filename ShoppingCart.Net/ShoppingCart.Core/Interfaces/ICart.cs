using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Global.ResponseWrapper;

namespace ShoppingCart.Core.Interfaces;

public interface ICart
{
    Response InsertProduct(ProductCreateModel productCreateModel);
    Response RemoveProduct(int itemId);
}