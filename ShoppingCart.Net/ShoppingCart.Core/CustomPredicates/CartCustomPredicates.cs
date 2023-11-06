using ShoppingCart.Core.Entity;

namespace ShoppingCart.Core.CustomPredicates;

public static class CartCustomPredicates
{
    public static bool DiscountEligibilityCheck(this Cart cart, Promotion promotion) =>
        promotion.PromotionConditionPredicate(cart);
}