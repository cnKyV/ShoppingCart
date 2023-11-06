using AutoMapper;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.ValueObjects;
using ShoppingCart.Global.Utility;
using ShoppingCart.Global.Utility.Settings;

namespace ShoppingCart.Core.Entity;

public partial class Cart : BaseEntity<Cart>, ICart
{
    public List<Product> Products { get; private set; }
    private HashSet<Promotion> _promotions = new();

    private static readonly ProductSettings ProductSettings = ConfigurationManager.ApplicationSettings.ProductSettings;
    private static readonly PromotionSettings PromotionSettings = ConfigurationManager.ApplicationSettings.PromotionSettings;

    private static readonly int MaximumUniqueItemsPossibleCount = ProductSettings.ProductDefaults.MaximumUniqueItemsPossibleCount;
    private static readonly int MaximumNumberOfElements = ProductSettings.ProductDefaults.MaximumNumberOfElements;
    private static readonly decimal TotalAmountOfCartUpperLimit = ProductSettings.ProductDefaults.TotalAmountOfCartUpperLimit;
    private static readonly int MaximumNumberOfDigitalItems = ProductSettings.DigitalProductDefaults.MaximumNumberOfDigitalItems;
    
    public int TotalDigitalItemsInCart => GetNumberOfTotalDigitalProductsInCart();
    public int TotalItemsInCart => GetNumberOfTotalProductsInCart();

    public Price Price { get; private set; }
    public Promotion? ActivePromotion { get; private set; }
    
    private readonly IMapper _mapper = Mappings.Mapper.GetMapper();

}