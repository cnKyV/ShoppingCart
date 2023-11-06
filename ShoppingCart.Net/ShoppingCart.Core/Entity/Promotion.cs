using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.ValueObjects;
using ShoppingCart.Global.Enums;

namespace ShoppingCart.Core.Entity;

public partial class Promotion : BaseEntity<Promotion>
{
    public int PromotionId { get; init; }
    public Discount Discount { get; init; }
    public Func<Cart, bool> PromotionConditionPredicate { get; init; }

    public PromotionScope PromotionScope { get; init; }
    public List<IProduct> AffectedProducts { get; private set; }
}