using System.Diagnostics;
using ShoppingCart.Core.Entity;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Contract.Interfaces;
using ShoppingCart.Global.Enums;

namespace ShoppingCart.Core.Factories;

public static class ProductFactory 
{
    public static IProduct CreateProduct(IProductCreateModel productCreateModel)
    {
        switch (productCreateModel)
        {
            case DigitalProductCreateModel digitalProductCreateModel:
                if (digitalProductCreateModel.ProductType is ProductType.DigitalProduct)
                    return new DigitalProduct(digitalProductCreateModel);
                break;
            
            case VasProductCreateModel vasProductCreateModel:
                if (vasProductCreateModel.ProductType is ProductType.VasProduct)
                    return new VasProduct(vasProductCreateModel);
                break;
                
            case ProductCreateModel defaultProductCreateModel:
                return new Product(defaultProductCreateModel);

            default: throw new ArgumentException("Invalid type", nameof(productCreateModel));
        }

        throw new InvalidOperationException("Unhandled exception occured.");
    }
}