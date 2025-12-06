namespace Gifts;

public sealed class VeryNiceBehavior : IBehavior
{
    public Toy ChooseToy(IReadOnlyList<Toy> wishlist)
        => wishlist[0];
}