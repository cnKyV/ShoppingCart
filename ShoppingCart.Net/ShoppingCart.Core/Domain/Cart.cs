using System.Collections.Concurrent;
using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Core.Consts.ErrorMessages;
using ShoppingCart.Core.Consts.SuccessMessages;
using ShoppingCart.Core.EqualityComparers;
using ShoppingCart.Core.Factories;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Utility;
using ShoppingCart.Core.ValueObjects;
using ShoppingCart.Core.CustomPredicates;
using ShoppingCart.Global.Enums;
using ShoppingCart.Global.ResponseWrapper;
using Mapper = AutoMapper.Mapper;

namespace ShoppingCart.Core.Entity;

public partial class Cart
{
    public Response InsertProduct(ProductCreateModel productCreateModel)
    {
       var validation = CheckCartProductValidations(productCreateModel);

       if (!validation.Result)
           return validation;
           
       
       
       var product = ProductFactory.CreateProduct(productCreateModel);

       switch (product)
       { 
           case DigitalProduct digitalProduct:
               
               Products.Add(digitalProduct);

               break;
           
           case Product physicalProduct:
               
               Products.Add(physicalProduct);
               
               break;
       }

       product.PriceUpdated += Product_PriceUpdated;
       product.BeforeAddingVasProduct += VasProduct_CartDiscountPriceCheckBeforeAddingVasProduct;
        RecalculatePrice();

       var result = ResponseWrapper.Success(CartDomainSuccessConsts.ProductAddSuccess);
       return result;
    }

    public Response RemoveProduct(int itemId)
    {
        var product = GetProductByItemId(itemId);

        if (product is null)
        {
            return ResponseWrapper.Error(CartDomainErrorConsts.NotFound);
        }

        switch (product)
        {
            case DigitalProduct digitalProduct:
                
                Products.Remove(digitalProduct);

                break;
            
            case Product defaultProduct:

                Products.Remove(defaultProduct);
                
                break;
        }

       RecalculatePrice();

       var result = ResponseWrapper.Success(CartDomainSuccessConsts.ProductRemoveSuccess);
       return result;
    }

    private IProduct? GetProductByItemId(int itemId)
    {
        var product = Products.FirstOrDefault(x => x.ItemId == itemId);

        return product ?? null;
    }

    private bool CheckIfCartIsEligibleForPromotion()
    {
        CheckCartPromotionValidations();
        
        _promotions.Clear();
        foreach (var promotion in MockData.Promotions)
        {
            var result = this.DiscountEligibilityCheck(promotion);
            
            if (result)
            {
                _promotions.Add(promotion);
            }
        }
        
        return _promotions.Any();
    }

    private Response? CheckCartProductValidations(ProductCreateModel productCreateModel)
    {
        var product = _mapper.Map<Product>(productCreateModel);
        var isFirstProduct = Products is null;
        

        var products = new List<Product>();

        if (isFirstProduct)
        {
            products.Add(product);
            product.Price.TotalPrice *= product.Quantity;
            Price = product.Price;

        }
        else
        {
            products.AddRange(Products);
        }
        
        void ResetCartState()
        {
            Products = products;
            RecalculatePrice();
        }

        ResetCartState();

        if (product.ProductType is ProductType.VasProduct)
        {
            ResetCartState();
            if (isFirstProduct)
                products.Remove(product);
            return ResponseWrapper.Error(CartDomainErrorConsts.NotValid);
        }
        
        if (product.ProductType is ProductType.DigitalProduct && IsMaximumNumberOfDigitalItemsExceeded(product))
        {
            ResetCartState();
            if (isFirstProduct)
                products.Remove(product);
            return ResponseWrapper.Error(CartDomainErrorConsts.MaximumDigitalNumberExceeded);
        }

        if (product.Quantity > 10)
        {
            ResetCartState();
            if (isFirstProduct)
                products.Remove(product);
            return ResponseWrapper.Error(CartDomainErrorConsts.QuantityExceeded);
        }

        if (IsMaximumNumberOfElementsExceeded(product))
        {
            ResetCartState();
            if (isFirstProduct)
                products.Remove(product);
            return ResponseWrapper.Error(
                CartDomainErrorConsts.MaximumNumberOfElementsExceeded);
        }

        if (IsMaximumNumberOfUniqueElementsExceeded())
        {
            ResetCartState();
            if (isFirstProduct)
                products.Remove(product);
            return ResponseWrapper.Error(
                CartDomainErrorConsts.MaximumNumberOfUniqueElementsExceeded);
        }
        
        if (IsAmountExceeded(productCreateModel.Price, isFirstProduct))
        {
            ResetCartState();
            if (isFirstProduct)
                products.Remove(product);
            return ResponseWrapper.Error(CartDomainErrorConsts.AmountExceeded);
        }
        if (isFirstProduct)
            products.Remove(product);
        ResetCartState();
        return ResponseWrapper.Success(CartDomainSuccessConsts.CartAddProductValidationSuccess);
    }

    private void CheckCartPromotionValidations()
    { 
        
    }

    
    private int GetNumberOfTotalProductsInCart()
    {
        ConcurrentBag<int> totalQuantity = new();

        Parallel.ForEach(Products, p =>
        {
            totalQuantity.Add(p.Quantity);
        });

        return totalQuantity.Sum();
    }
    
    private int GetNumberOfTotalDigitalProductsInCart()
    {
        ConcurrentBag<int> totalQuantity = new();

        Parallel.ForEach(Products, p =>
        {
            if (p.ProductType is ProductType.DigitalProduct)
            { 
                totalQuantity.Add(p.Quantity);
            }
            
        });

        return totalQuantity.Sum();
    }

    private bool IsMaximumNumberOfUniqueElementsExceeded()
    {
        var uniqueProductsCount = Products.Distinct(new UniqueProductEqualityComparer<Product>()).Count();
        return uniqueProductsCount >= MaximumUniqueItemsPossibleCount;
    }

    
    private Promotion? GetMostBeneficialPromotion()
    {
        Promotion lowestPricePromotion = null;
        decimal lowestPrice = decimal.MaxValue;
        var price = new Price
        {
            TotalPrice = Price.TotalPrice,
            TotalDiscount = default
        };
        //TODO ApplyMostBeneficialPromotion -> do the calc logic here along with 
        foreach (var promotion in _promotions)
        {
            promotion.CalculateDiscountedPrice(price);
            if (price.DiscountedPrice < lowestPrice && promotion.PromotionScope is PromotionScope.Cart)
            {
                lowestPrice = price.DiscountedPrice;
                lowestPricePromotion = promotion;
            }
        }

        return lowestPricePromotion;
    }

    private void ApplyPromotion()
    {
        var isEligibleForPromotion = CheckIfCartIsEligibleForPromotion();

        if (isEligibleForPromotion)
        {
            var promotion = GetMostBeneficialPromotion();

            if (promotion.PromotionScope == PromotionScope.Product)
            {
                var products = Products.Where(x =>
                    PromotionSettings.CategoryPromotionDefaults.CategoryPromotionEligibleCategoryIds.Contains(
                        x.CategoryId));

                foreach (var product in products)
                {
                    product.AddPromotion(promotion);
                }

                return;
            }

            ActivePromotion = promotion;
        }
    }    
    
    private void RecalculatePrice()
    {
        ApplyPromotion();
        
        Price.TotalPrice = default;
        Price.TotalDiscount = default;
        var productLevelPromotion = false;

        foreach (var productPrice in Products.Select(product => product.GetPricesOfProductIncludingVasAndQuantity()))
        {
            Price.TotalPrice += productPrice.TotalPrice;
            Price.TotalDiscount += productPrice.TotalDiscount;
        }

        if (Price.TotalDiscount == default && ActivePromotion is not null)
        {
            ActivePromotion.CalculateDiscountedPrice(Price);
        }
    }

    private void Product_PriceUpdated(object? sender, EventArgs e)
    {
        RecalculatePrice();
    }

    private void VasProduct_CartDiscountPriceCheckBeforeAddingVasProduct(object sender, CanAddContainer container)
    {
        var response = IsAmountExceeded(Price.DiscountedPrice, false);
       container.CanAdd = response;
    }

    private bool IsMaximumNumberOfElementsExceeded(Product product)
    {
        return product.Quantity+TotalItemsInCart > MaximumNumberOfElements;
    }

    private bool IsAmountExceeded(decimal totalPrice, bool isIinitial)
    {
        bool result;

        if (isIinitial)
            result = totalPrice - Price.TotalDiscount >= TotalAmountOfCartUpperLimit;
        else
            result = totalPrice + Price.DiscountedPrice >= TotalAmountOfCartUpperLimit;

        return result;
    }

    private bool IsMaximumNumberOfDigitalItemsExceeded(Product product)
    {
        var digitalItems = GetNumberOfTotalDigitalProductsInCart();

        return digitalItems > MaximumNumberOfDigitalItems;
    }

    public Response ResetCart()
    {
        Products.Clear();
        RecalculatePrice();
        ActivePromotion = null;
        return ResponseWrapper.Success(CartDomainSuccessConsts.CartResetSuccess);
    }
}