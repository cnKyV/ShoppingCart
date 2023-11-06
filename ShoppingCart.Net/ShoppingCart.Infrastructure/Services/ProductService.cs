using AutoMapper;
using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Contract.RequestModels;
using ShoppingCart.Core.Entity;
using ShoppingCart.Global.ResponseWrapper;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Services;

public class ProductService : IProductService
{
    private List<Product> _product;
    private readonly IMapper _mapper = ShoppingCart.Core.Mappings.Mapper.GetMapper();
    public ProductService()
    {
        _product = new List<Product>();
    }

    public Response AddVasProduct(VasProductInsertRequestModel vasProductInsertRequestModel)
    {
        throw new NotImplementedException();
    }

    public Response RemoveVasProduct(int itemId)
    {
        throw new NotImplementedException();
    }
}