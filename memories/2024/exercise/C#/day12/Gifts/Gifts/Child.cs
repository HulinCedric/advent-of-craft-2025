using LanguageExt;

namespace Gifts;

public sealed class Child
{
    private readonly Behavior _behavior;
    private readonly ChildName _name;
    private readonly WishList _wishlist;

    public Child(ChildName name, Behavior behavior) : this(name, behavior, WishList.Empty())
    {
    }

    private Child(ChildName name, Behavior behavior, WishList wishlist)
    {
        _name = name;
        _behavior = behavior;
        _wishlist = wishlist;
    }

    public Child SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
        => new(_name, _behavior, WishList.WithThreeChoices(firstChoice, secondChoice, thirdChoice));

    internal Option<Toy> GetChoice() => _behavior.GetChoice(_wishlist);

    internal bool IsNamed(ChildName childName) => _name == childName;
}