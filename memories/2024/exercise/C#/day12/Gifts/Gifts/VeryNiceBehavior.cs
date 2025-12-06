namespace Gifts;

public sealed class VeryNiceBehavior : IBehavior
{
    public Toy? ChooseToy(IReadOnlyList<Toy> wishlist)
    {
        if (wishlist == null || wishlist.Count == 0) return null;
        // Very nice children get the first choice
        return wishlist[0];
    }
}

