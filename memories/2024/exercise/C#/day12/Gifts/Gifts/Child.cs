using LanguageExt;

namespace Gifts;

public class Behavior(string value)
{
    public static readonly Behavior VeryNice = new VeryNiceBehavior();
    public static readonly Behavior Nice = new NiceBehavior();
    public static readonly Behavior Naughty = new NaughtyBehavior();

    internal virtual Option<Toy> GetChoice(WishList wishList) => Option<Toy>.None;
}

internal class NaughtyBehavior : Behavior
{
    private const string Value = "naughty";

    internal NaughtyBehavior() : base(Value)
    {
    }

    internal override Option<Toy> GetChoice(WishList wishList) => wishList.GetThirdChoice();
}

internal class NiceBehavior : Behavior
{
    private const string Value = "nice";

    internal NiceBehavior() : base(Value)
    {
    }

    internal override Option<Toy> GetChoice(WishList wishList) => wishList.GetSecondChoice();
}

internal class VeryNiceBehavior : Behavior
{
    private const string Value = "very nice";

    internal VeryNiceBehavior() : base(Value)
    {
    }

    internal override Option<Toy> GetChoice(WishList wishList) => wishList.GetFirstChoice();
}

public record ChildName(string Name)
{
    public static implicit operator ChildName(string name) => new(name);
}

public class Child
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