namespace Gifts;

public sealed class NiceBehavior : IBehavior
{
    public Toy ChooseToy(IReadOnlyList<Toy> wishlist)
        => wishlist[1];
}