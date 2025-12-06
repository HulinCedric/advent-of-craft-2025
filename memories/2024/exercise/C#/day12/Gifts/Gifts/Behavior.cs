using LanguageExt;

namespace Gifts;

public class Behavior(string value)
{
    public static readonly Behavior VeryNice = new VeryNiceBehavior();
    public static readonly Behavior Nice = new NiceBehavior();
    public static readonly Behavior Naughty = new NaughtyBehavior();

    internal virtual Option<Toy> GetChoice(WishList wishList) => Option<Toy>.None;
}