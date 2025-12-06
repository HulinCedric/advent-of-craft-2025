namespace Gifts;

public interface IBehavior
{
    Toy ChooseToy(IReadOnlyList<Toy> wishlist);
}