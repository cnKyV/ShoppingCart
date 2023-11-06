using System.ComponentModel;

namespace ShoppingCart.Global.Enums;

public enum PromotionScope
{
    [Description("Cart")]
    Cart = 0,
    [Description("Product")]
    Product = 1
}