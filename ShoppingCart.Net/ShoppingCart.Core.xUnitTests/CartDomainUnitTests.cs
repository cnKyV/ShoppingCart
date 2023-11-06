using System.Collections;
using System.Diagnostics.Metrics;
using AutoBogus;
using Bogus;
using FluentAssertions;
using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Contract.Interfaces;
using ShoppingCart.Core.Consts.ErrorMessages;
using ShoppingCart.Core.Consts.SuccessMessages;
using ShoppingCart.Core.Entity;
using ShoppingCart.Global.Enums;
using ShoppingCart.Global.ResponseWrapper;

namespace ShoppingCart.Core.xUnitTests;

public class CartDomainUnitTests
{
    [Theory]
    [MemberData(nameof(CartValidationsTestData_MaximumNumberOfElementsValidation))]
    public void CheckCartValidations_MustFailOnExceedingNumbers(List<ProductCreateModel> productCreateModels)
    {
        var cart = new Cart();
        Response response = ResponseWrapper.Success("default");

        foreach (var productCreateModel in productCreateModels)
        {
            response = cart.InsertProduct(productCreateModel);
        }

        response.Result.Should().BeFalse();
        response.Message.Should().Be(CartDomainErrorConsts.MaximumNumberOfElementsExceeded);
    }

    [Fact]
    public void CheckCartValidations_MustInsertProductSuccessfully()
    {
        var productCreateModel = new ProductCreateModel
        {
            Price = 200,
            Quantity = 5,
            CategoryId = 1,
            ItemId = 1,
            ProductType = ProductType.DefaultProduct,
            SellerId = 1
        };

        var cart = new Cart();
        var response = cart.InsertProduct(productCreateModel);

        response.Result.Should().BeTrue();
        response.Message.Should().Be(CartDomainSuccessConsts.ProductAddSuccess);
    }

    [Theory]
    [MemberData(nameof(CartValidationsTestData_MaximumNumberOfDigitalProductValidation))]
    public void CheckCartValidations_MustFailOnExceedingDigitalNumbers(List<ProductCreateModel> productCreateModels)
    {
        var cart = new Cart();
        Response response = ResponseWrapper.Success("default");

        foreach (var productCreateModel in productCreateModels)
        {
            response = cart.InsertProduct(productCreateModel);
        }

        response.Result.Should().BeFalse();
        response.Message.Should().Be(CartDomainErrorConsts.MaximumDigitalNumberExceeded);
    }    
    
    [Theory]
    [MemberData(nameof(CartValidationsTestData_MaximumNumberOfUniqueElementsValidation))]
    public void CheckCartValidations_MustFailOnExceedingUniqueNumbers(List<ProductCreateModel> productCreateModels)
    {
        var cart = new Cart();
        Response response = ResponseWrapper.Success("default");

        foreach (var productCreateModel in productCreateModels)
        {
            response = cart.InsertProduct(productCreateModel);
        }

        response.Result.Should().BeFalse();
        response.Message.Should().Be(CartDomainErrorConsts.MaximumNumberOfUniqueElementsExceeded);
    }

    [Fact]
    public void CheckCartValidations_MustFailVasProduct()
    {
        var productCreateModel = new ProductCreateModel
        {
            Price = 200,
            Quantity = 5,
            CategoryId = 1,
            ItemId = 1,
            ProductType = ProductType.VasProduct,
            SellerId = 1
        };

        var cart = new Cart();
        var response = cart.InsertProduct(productCreateModel);

        response.Result.Should().BeFalse();
        response.Message.Should().Be(CartDomainErrorConsts.NotValid);
    }

    public static IEnumerable<object[]> CartValidationsTestData_VasProductValidation
    {
        get
        {
            yield return new object[]
            {
                new ProductCreateModel
                {
                    Price = 200,
                    Quantity = 5,
                    CategoryId = 1,
                    ItemId = 1,
                    ProductType = ProductType.VasProduct,
                    SellerId = 1
                }
            };
        }
    }
    public static IEnumerable<object[]> CartValidationsTestData_MaximumNumberOfElementsValidation
    {
        get
        {
            var generator = new AutoFaker<ProductCreateModel>();
            yield return new object[]
            {
                generator
                    .RuleFor(x => x.Quantity, faker => faker.Random.Int(1, 1))
                    .RuleFor(x=>x.ProductType, ProductType.DefaultProduct)
                    .RuleFor(x => x.ItemId, faker => faker.Random.Int(1, 1))
                    .RuleFor(x => x.Price, faker => faker.Random.Decimal(1,1))
                    .Generate(31)
            };
        }
    }    
    
    public static IEnumerable<object[]> CartValidationsTestData_MaximumNumberOfUniqueElementsValidation
    {
        get
        {
            var generator = new AutoFaker<ProductCreateModel>();
            var idCounter = 1;
            yield return new object[]
            {
                generator.RuleFor(x => x.ItemId, () => idCounter++)
                    .RuleFor(x => x.Quantity, faker => faker.Random.Int(1, 1))
                    .RuleFor(x=>x.ProductType, ProductType.DefaultProduct)
                    .RuleFor(x => x.Price, faker => faker.Random.Decimal(1,1))
                    .Generate(11)
            };
        }
    }    
    
    public static IEnumerable<object[]> CartValidationsTestData_MaximumNumberOfDigitalProductValidation
    {
        get
        {
            var generator = new AutoFaker<ProductCreateModel>();
            var idCounter = 1;
            yield return new object[]
            {
                generator.RuleFor(x => x.ItemId, () => idCounter++)
                    .RuleFor(x => x.Quantity, faker => faker.Random.Int(1, 1))
                    .RuleFor(x=>x.ProductType, ProductType.DigitalProduct)
                    .RuleFor(x => x.Price, faker => faker.Random.Decimal(1,1))
                    .Generate(6)
            };
        }
    }
}