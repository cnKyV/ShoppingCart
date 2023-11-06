using ShoppingCart.Global.Utility.Settings;
using ShoppingCart.Global.Utility;
using ShoppingCart.Contract.RequestModels;
using ShoppingCart.Core.Consts.ErrorMessages;
using ShoppingCart.Core.Consts.SuccessMessages;
using ShoppingCart.Global.ResponseWrapper;

namespace ShoppingCart.Core.Validators;

public class DefaultItemValidator
{
    private static readonly DigitalProductDefaults DigitalProductDefaults = ConfigurationManager.ApplicationSettings.ProductSettings.DigitalProductDefaults;

    public static Response ValidateDefaultProduct(ProductInsertRequestModel productInsertRequestModel)
    {
        if (DigitalProductDefaults.DigitalProductEligibleProductCategoryIds.Contains(
                productInsertRequestModel.CategoryId))
            return ResponseWrapper.Error(DefaultItemValidatorErrorConsts.WrongProductCategoryId);

        return ResponseWrapper.Success(DefaultItemValidatorSuccessConsts.DefaultItemValidationSuccess);
    }
}