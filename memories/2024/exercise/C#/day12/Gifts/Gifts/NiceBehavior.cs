using LanguageExt;

namespace Gifts;

internal class NiceBehavior : Behavior
{
    internal override Option<Toy> GetChoice(WishList wishList) => wishList.GetSecondChoice();
}