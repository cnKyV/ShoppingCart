namespace ShoppingCart.Contract.DomainModels.UpdateModels;

public record ProductUpdateModel
{
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}