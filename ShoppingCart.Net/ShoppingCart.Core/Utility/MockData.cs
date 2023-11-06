using ShoppingCart.Core.Entity;
using ShoppingCart.Core.ValueObjects;
using ShoppingCart.Global.Enums;
using ShoppingCart.Global.Utility;
using ShoppingCart.Global.Utility.Settings;

namespace ShoppingCart.Core.Utility;

public static class MockData
{
    static MockData()
    {
        PromotionSettings = ConfigurationManager.ApplicationSettings.PromotionSettings;
    }

    private static readonly PromotionSettings PromotionSettings;
    public static List<Promotion> Promotions { get; } = new()
    {
        new Promotion
        {
            PromotionId = 9909,
            Discount = new Discount { Amount = 10, DiscountType = DiscountType.Percentage },
            PromotionConditionPredicate = cart => cart.Products.All(p => p.SellerId == cart.Products.First().SellerId) && cart.Products.Count > 1
        },
        new Promotion
        {
            PromotionId = 5676,
            Discount = new Discount { Amount = 5, DiscountType = DiscountType.Percentage },
            PromotionConditionPredicate = cart => cart.Products.Any(x=>x.CategoryId == 3003)
        },
        new Promotion
        {
            PromotionId = 1232,
            Discount = new Discount{Amount = 250, DiscountType = DiscountType.Flat},
            PromotionConditionPredicate = cart => cart.Price.TotalPrice is >= 500 and < 5000
        },
        new Promotion
        {
            PromotionId = 1232,
            Discount = new Discount{Amount = 500, DiscountType = DiscountType.Flat},
            PromotionConditionPredicate = cart => cart.Price.TotalPrice is >= 5000 and < 10000
        },
        new Promotion
        {
            PromotionId = 1232,
            Discount = new Discount{Amount = 1000, DiscountType = DiscountType.Flat},
            PromotionConditionPredicate = cart => cart.Price.TotalPrice is >= 10000 and < 50000
        },
        new Promotion
        {
            PromotionId = 1232,
            Discount = new Discount{Amount = 2000, DiscountType = DiscountType.Flat},
            PromotionConditionPredicate = cart => cart.Price.TotalPrice >= 50000
        }
        
    };
    
    
}