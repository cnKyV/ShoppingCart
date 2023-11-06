using ShoppingCart.Contract.RequestModels;
using ShoppingCart.Core.Consts.ErrorMessages;
using ShoppingCart.Core.Consts.SuccessMessages;
using ShoppingCart.Global.ResponseWrapper;
using ShoppingCart.Global.Utility;
using ShoppingCart.Global.Utility.Settings;

namespace ShoppingCart.Core.Validators;

public static class VasItemValidator
{
    private static readonly VasProductDefaults VasProductDefaults = ConfigurationManager.ApplicationSettings.ProductSettings.VasProductDefaults;
    private static readonly DigitalProductDefaults DigitalProductDefaults = ConfigurationManager.ApplicationSettings.ProductSettings.DigitalProductDefaults;
    public static Response ValidateVasProduct(VasProductInsertRequestModel vasProductInsertRequestModel)
    {

        if (VasProductDefaults.VasProductDefaultSellerId != vasProductInsertRequestModel.VasSellerId)
            return ResponseWrapper.Error(VasItemValidatorErrorConsts.IncorrectSellerId);

        if (DigitalProductDefaults.DigitalProductEligibleProductCategoryIds.Contains(
                vasProductInsertRequestModel.VasCategoryId))
            return ResponseWrapper.Error(DefaultItemValidatorErrorConsts.WrongProductCategoryId);

        return ResponseWrapper.Success(VasItemValidatorSuccessConsts.VasItemValidationSuccess);
    }
}