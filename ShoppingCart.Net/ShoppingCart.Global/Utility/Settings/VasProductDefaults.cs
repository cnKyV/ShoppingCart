namespace ShoppingCart.Global.Utility.Settings;

public class VasProductDefaults
{
    public int VasProductDefaultSellerId { get; set; }
    public int VasProductDefaultCategoryId { get; set; }
    public List<int> VasProductEligibleProductCategoryIds { get; set; }
    public int MaximumNumberOfVasProductsCanBeAppliedToProduct { get; set; }
}