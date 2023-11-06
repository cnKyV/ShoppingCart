namespace ShoppingCart.Core.ValueObjects;

public class Price
{
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal DiscountedPrice => TotalPrice - TotalDiscount;
}