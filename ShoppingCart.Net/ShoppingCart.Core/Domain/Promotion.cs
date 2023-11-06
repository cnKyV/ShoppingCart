using ShoppingCart.Core.ValueObjects;
using ShoppingCart.Global.Enums;

namespace ShoppingCart.Core.Entity;

public partial class Promotion
{

    public void CalculateDiscountedPrice(Price price) //TODO should this be in Price object? By Design?
    {
        if (price.TotalPrice < 0)
        {
            //totalPrice can not be lower than 0
        }

        var isFlatDiscount = Discount.DiscountType == DiscountType.Flat;
        var promotionAmount = Discount.Amount;

        if (isFlatDiscount)
        {
            price.TotalDiscount = promotionAmount;
            return;
        }

        var discountAmount = (price.TotalPrice  * promotionAmount) / 100;
        price.TotalDiscount = discountAmount;
    }
}