namespace ShoppingCart.Core.Consts.ErrorMessages;

public static class CartDomainErrorConsts
{
    public const string NotFound = "Product is not found.";
    public const string NotValid = "Product type is not a valid.";
    public const string MaximumDigitalNumberExceeded = "Maximum number of digital items has exceeded.";
    public const string MaximumNumberOfElementsExceeded = "Number of products is at its limit. You cannot insert more products into the cart.";
    public const string MaximumNumberOfUniqueElementsExceeded = "Number of unique product is at its limit. You cannot insert any other type of product into the cart.";
    public const string AmountExceeded = "Cart amount is exceeded, can not add more products.";
    public const string QuantityExceeded = "Product can not have quantity that is more than 10";

}