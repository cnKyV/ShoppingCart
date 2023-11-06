namespace ShoppingCart.Contract.Dto;

public record ProductDto
{
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public IEnumerable<VasProductDto> VasProducts { get; set; }
}