using LanguageExt;

namespace Gifts;

internal class NaughtyBehavior : Behavior
{
    internal override Option<Toy> GetChoice(WishList wishList) => wishList.GetThirdChoice();
}