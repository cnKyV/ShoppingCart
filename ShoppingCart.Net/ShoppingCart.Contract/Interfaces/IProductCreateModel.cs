using ShoppingCart.Global.Enums;

namespace ShoppingCart.Contract.Interfaces;

public interface IProductCreateModel
{
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public ProductType ProductType { get; set; }
}