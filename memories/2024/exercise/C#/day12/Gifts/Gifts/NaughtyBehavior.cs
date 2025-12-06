namespace Gifts;

public sealed class NaughtyBehavior : IBehavior
{
    public Toy ChooseToy(IReadOnlyList<Toy> wishlist)
        => wishlist[^1];
}