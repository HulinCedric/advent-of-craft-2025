namespace Gifts;

public sealed class NiceBehavior : IBehavior
{
    public Toy? ChooseToy(IReadOnlyList<Toy> wishlist)
    {
        if (wishlist == null || wishlist.Count < 2) return null;
        // Nice children get the second choice
        return wishlist[1];
    }
}

