namespace ShoppingCart.Contract.Dto;

public record VasProductDto
{
    public int VasItemId { get; set; }
    public int VasCategoryId { get; set; }
    public int VasSellerId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}