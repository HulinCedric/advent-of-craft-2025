using LanguageExt;

namespace Gifts;

internal sealed class NiceBehavior : Behavior
{
    internal override Option<Toy> GetChoice(WishList wishList) => wishList.GetSecondChoice();
}