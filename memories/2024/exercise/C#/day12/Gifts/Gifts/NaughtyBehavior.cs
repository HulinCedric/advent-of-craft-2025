using LanguageExt;

namespace Gifts;

internal sealed class NaughtyBehavior : Behavior
{
    internal override Option<Toy> GetChoice(WishList wishList) => wishList.GetThirdChoice();
}