using LanguageExt;

namespace Gifts;

internal class WishList
{
    private readonly Seq<Toy> _wishlist;

    private WishList() : this([])
    {
    }

    private WishList(params Toy[] toys) => _wishlist = new Seq<Toy>(toys);

    internal static WishList WithThreeChoices(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
        => new(firstChoice, secondChoice, thirdChoice);

    internal static WishList Empty() => new();

    internal Option<Toy> GetFirstChoice() => _wishlist.At(0);

    internal Option<Toy> GetSecondChoice() => _wishlist.At(1);

    internal Option<Toy> GetThirdChoice() => _wishlist.At(2);
}