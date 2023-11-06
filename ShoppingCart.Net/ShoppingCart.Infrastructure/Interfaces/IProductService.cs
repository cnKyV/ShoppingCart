using ShoppingCart.Contract.RequestModels;
using ShoppingCart.Global.ResponseWrapper;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface IProductService
{
    Response AddVasProduct(VasProductInsertRequestModel vasProductInsertRequestModel);
    Response RemoveVasProduct(int itemId);

}