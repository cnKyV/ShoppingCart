using ShoppingCart.Core.Consts.ErrorMessages;
using ShoppingCart.Core.Consts.SuccessMessages;
using ShoppingCart.Core.ValueObjects;
using ShoppingCart.Contract.DomainModels.CreateModels;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Global.Enums;
using ShoppingCart.Global.ResponseWrapper;
using ShoppingCart.Global.Utility;
using ShoppingCart.Global.Utility.Settings;

namespace ShoppingCart.Core.Entity;

public partial class Product
{
    private readonly FeatureFlags _featureFlags = ConfigurationManager.ApplicationSettings.FeatureFlags;
    private readonly ProductSettings _productSettings = ConfigurationManager.ApplicationSettings.ProductSettings;
    internal HashSet<Promotion?> Promotions { get; private set; } = new();
    internal List<VasProduct> VasProducts { get; private set; } = new();
    public bool HasProductLevelDiscount { get; set; } = false;
    public Price GetPricesOfProductIncludingVasAndQuantity()
    {
        var price = new Price();
        
        price.TotalPrice = Price.TotalPrice * Quantity;

        foreach (var promotion in Promotions)
        {
            promotion.CalculateDiscountedPrice(price);

        }

        return price;
    }
    public Response AddVasProduct(VasProduct vasProduct)
    {
         var validationResult = CheckProductVasProductValidations(vasProduct);

         if (!validationResult.Result)
             return validationResult;

        //check current cart price

        var canAddContainer = new CanAddContainer { CanAdd = false };
        BeforeAddingVasProduct.Invoke(this, canAddContainer);

        if (!canAddContainer.CanAdd)
        {
            VasProducts.Add(vasProduct);
            RecalculatePrice();

            return ResponseWrapper.Success(ProductDomainSuccessConsts.AddVasProductSuccess);
        }

        return ResponseWrapper.Error(ProductDomainErrorConsts.AmountExceeded);
    }

    public void AddPromotion(Promotion promotion)
    {
        CheckProductPromotionValidations(promotion);
        Promotions.Add(promotion);
        HasProductLevelDiscount = true;
        RecalculatePrice();
    }

    private Response CheckProductVasProductValidations(VasProduct vasProduct)
    {
        if (VasProductMaximumNumberOfPossibleElementsExceeded)
        {
            return ResponseWrapper.Error(ProductDomainErrorConsts.MaximumNumberOfVasElementsExceeded);
        }

        if (!VasProductDefaults.VasProductEligibleProductCategoryIds.Contains(CategoryId))
            return ResponseWrapper.Error(VasItemValidatorErrorConsts.WrongProductCategoryId);

        return ResponseWrapper.Success(ProductDomainSuccessConsts.ProductAddVasProductValidationSuccess);
    }

    private void CheckProductPromotionValidations(Promotion promotion)
    {
        
    }

    private void RecalculatePrice()
    {
        ////Price.TotalPrice *= Quantity;
        decimal vasTotalPrice = default;
        foreach (var vasProduct in VasProducts)
        {
            vasTotalPrice += vasProduct.Price;
        }

        if (_featureFlags.ShouldIncludeVasItemsForPromotionCalculation)
            Price.TotalPrice += vasTotalPrice;


        foreach (var promotion in Promotions)
        {
            promotion.CalculateDiscountedPrice(Price);

        }

        if (!_featureFlags.ShouldIncludeVasItemsForPromotionCalculation)
            Price.TotalPrice += vasTotalPrice;

        OnPriceUpdated();
    }

    private void OnPriceUpdated() => PriceUpdated?.Invoke(this, EventArgs.Empty);
}