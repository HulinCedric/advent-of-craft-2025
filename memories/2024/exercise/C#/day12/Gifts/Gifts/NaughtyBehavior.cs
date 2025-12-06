namespace Gifts;

public sealed class NaughtyBehavior : IBehavior
{
    public Toy? ChooseToy(IReadOnlyList<Toy> wishlist)
    {
        if (wishlist == null || wishlist.Count == 0) return null;
        // Naughty children get the last choice
        return wishlist[^1];
    }
}

