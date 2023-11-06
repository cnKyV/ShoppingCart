using ShoppingCart.Contract.RequestModels;
using ShoppingCart.Contract.ResponseModels;
using ShoppingCart.Global.ResponseWrapper;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface ICartService
{
    Response<DisplayCartResponseModel> DisplayCart();
    Response AddProduct(ProductInsertRequestModel productInsertRequestModel);
    Response RemoveProduct(int itemId);
    Response ResetCart();
}