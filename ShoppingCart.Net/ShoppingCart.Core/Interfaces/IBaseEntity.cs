namespace ShoppingCart.Core.Interfaces;

public interface IBaseEntity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public string CreatedBy { get; init; }
    public string? UpdatedBy { get; init; }
    public bool? IsDeleted { get; set; }
}