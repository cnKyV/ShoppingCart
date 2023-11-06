using ShoppingCart.Core.Entity;

namespace ShoppingCart.Core.EqualityComparers;

public class UniqueProductEqualityComparer<T> : IEqualityComparer<T> where T : Product
{
    public bool Equals(T? x, T? y)
    {
        if (x is null && y is null)
            return false;

        if (x?.ItemId != y?.ItemId)
        {
            return false;
        }

        return true;
    }

    public int GetHashCode(T obj)
    {
        if (obj.ItemId == 0)
            return obj.ItemId;

        int hashCode = obj.ItemId.GetHashCode();
        return hashCode;
    }
}