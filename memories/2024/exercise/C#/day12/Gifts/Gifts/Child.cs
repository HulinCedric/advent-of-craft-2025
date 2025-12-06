using LanguageExt;

namespace Gifts;

public sealed class Child
{
    private readonly Behavior _behavior;
    private readonly WishList _wishlist;

    public Child(ChildName name, Behavior behavior) : this(name, behavior, WishList.Empty())
    {
    }

    private Child(ChildName name, Behavior behavior, WishList wishlist)
    {
        Name = name;
        _behavior = behavior;
        _wishlist = wishlist;
    }

    public ChildName Name { get; }

    public Child SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
        => new(Name, _behavior, WishList.WithThreeChoices(firstChoice, secondChoice, thirdChoice));

    internal Option<Toy> GetChoice() => _behavior.GetChoice(_wishlist);
}