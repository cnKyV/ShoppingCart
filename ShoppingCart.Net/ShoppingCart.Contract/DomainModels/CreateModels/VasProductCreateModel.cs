using ShoppingCart.Contract.Interfaces;
using ShoppingCart.Global.Enums;

namespace ShoppingCart.Contract.DomainModels.CreateModels;

public class VasProductCreateModel : IProductCreateModel
{
    public int AppliedProductId { get; set; }
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public ProductType ProductType { get; set; }
}