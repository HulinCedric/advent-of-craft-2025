namespace Gifts;

public interface IBehavior
{
    /// <summary>
    /// Choose a toy from the provided wishlist according to the behavior's policy.
    /// The wishlist may be empty; implementations should return null in that case.
    /// </summary>
    Toy? ChooseToy(IReadOnlyList<Toy> wishlist);
}

