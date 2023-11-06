using System.ComponentModel;

namespace ShoppingCart.Global.Enums;

public enum ProductType
{
    [Description("DefaultProduct")]
    DefaultProduct = 0,
    [Description("DigitalProduct")]
    DigitalProduct = 7889,
    [Description("VasProduct")]
    VasProduct = 3242
}