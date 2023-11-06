using System.ComponentModel;

namespace ShoppingCart.Global.Enums;

public enum DiscountType
{
    [Description("Flat")]
    Flat = 0,
    [Description("Percentage")]
    Percentage = 1
}