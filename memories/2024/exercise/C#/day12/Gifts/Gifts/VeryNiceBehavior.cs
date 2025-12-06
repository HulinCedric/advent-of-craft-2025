using LanguageExt;

namespace Gifts;

internal sealed class VeryNiceBehavior : Behavior
{
    internal override Option<Toy> GetChoice(WishList wishList) => wishList.GetFirstChoice();
}