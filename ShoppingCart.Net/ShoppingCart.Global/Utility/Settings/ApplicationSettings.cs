namespace ShoppingCart.Global.Utility.Settings;

public class ApplicationSettings
{
    public ProductSettings ProductSettings { get; set; }
    public PromotionSettings PromotionSettings { get; set; }
    public FeatureFlags FeatureFlags { get; set; }
}