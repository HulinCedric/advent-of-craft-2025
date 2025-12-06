using LanguageExt;

namespace Gifts;

internal class VeryNiceBehavior : Behavior
{
    internal override Option<Toy> GetChoice(WishList wishList) => wishList.GetFirstChoice();
}