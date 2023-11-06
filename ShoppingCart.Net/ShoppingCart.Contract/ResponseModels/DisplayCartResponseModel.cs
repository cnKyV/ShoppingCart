using ShoppingCart.Contract.Dto;

namespace ShoppingCart.Contract.ResponseModels;

public record DisplayCartResponseModel
{
    public IEnumerable<ProductDto> Products { get; set; }
    public decimal TotalAmount { get; set; }
    public int? AppliedPromotionId { get; set; }
    public decimal TotalDiscount { get; set; }
}