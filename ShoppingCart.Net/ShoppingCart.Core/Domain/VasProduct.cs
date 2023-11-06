using ShoppingCart.Global.Enums;

namespace ShoppingCart.Core.Entity;

public partial class VasProduct
{
    public int ItemId { get; private set; }
    public int AppliedItemId { get; set; }
    public int CategoryId { get; private set; }
    public int SellerId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public ProductType ProductType { get; init; }
}