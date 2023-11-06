using ShoppingCart.Global.Enums;

namespace ShoppingCart.Core.ValueObjects;

public class Discount
{
    public decimal Amount { get; set; }
    public DiscountType DiscountType { get; set; }
}