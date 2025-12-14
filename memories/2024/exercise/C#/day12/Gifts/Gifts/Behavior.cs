using LanguageExt;

namespace Gifts;

public abstract class Behavior
{
    public static readonly Behavior VeryNice = new VeryNiceBehavior();
    public static readonly Behavior Nice = new NiceBehavior();
    public static readonly Behavior Naughty = new NaughtyBehavior();

    internal abstract Option<Toy> GetChoice(WishList wishList);
}