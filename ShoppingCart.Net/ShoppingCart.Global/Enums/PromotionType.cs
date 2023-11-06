using System.ComponentModel;

namespace ShoppingCart.Global.Enums;

public enum PromotionType
{
    [Description("SameSellerPromotion")]
    SameSellerPromotion = 9909,
    [Description("CategoryPromotion")]
    CategoryPromotion = 5676,
    [Description("TotalPricePromotion")]
    TotalPricePromotion = 1232
}