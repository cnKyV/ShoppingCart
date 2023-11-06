using AutoMapper;
using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Contract.RequestModels;
using ShoppingCart.Contract.ResponseModels;
using ShoppingCart.Core.Consts.ErrorMessages;
using ShoppingCart.Core.Entity;
using ShoppingCart.Core.Validators;
using ShoppingCart.Global.ResponseWrapper;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Services;

public class CartService : ICartService
{
    private Cart _cart;
    private readonly IMapper _mapper = ShoppingCart.Core.Mappings.Mapper.GetMapper();
    
    public CartService()
    {
        _cart = new Cart();
    }
    public Response<DisplayCartResponseModel> DisplayCart()
    {
        var cart = _mapper.Map<DisplayCartResponseModel>(_cart);
        return ResponseWrapper.SuccessWithData(cart);
    }

    public Response AddProduct(ProductInsertRequestModel productInsertRequestModel)
    {
        var validatorResponse = DefaultItemValidator.ValidateDefaultProduct(productInsertRequestModel);
        if (!validatorResponse.Result)
            return validatorResponse;

        var productCreateModel = _mapper.Map<ProductCreateModel>(productInsertRequestModel);
        var response = _cart.InsertProduct(productCreateModel);

        return response;
    }

    public Response AddVasProduct(VasProductInsertRequestModel vasProductInsertRequestModel)
    {
        var validatorResponse = VasItemValidator.ValidateVasProduct(vasProductInsertRequestModel);

        if (!validatorResponse.Result)
            return validatorResponse;
        
        var vasProduct = _mapper.Map<VasProduct>(vasProductInsertRequestModel);

        if (_cart.Products is null || !_cart.Products.Any())
            return ResponseWrapper.Error(CartServiceErrorConsts.VasProductNoProductFound);
            

        var product = _cart.Products.FirstOrDefault(x => x.ItemId == vasProduct.AppliedItemId);

        if (product is null) 
            return ResponseWrapper.Error(CartServiceErrorConsts.ProductNotFound);
        
        
        var response = product.AddVasProduct(vasProduct);
        return response;
    }

    public Response RemoveProduct(int itemId)
    {
        var response = _cart.RemoveProduct(itemId);
        return response;
    }

    public Response ResetCart()
    {
        var response = _cart.ResetCart();
        return response;
    }
}