using LanguageExt;

namespace Gifts;

internal class VeryNiceBehavior : Behavior
{
    private const string Value = "very nice";

    internal VeryNiceBehavior() : base(Value)
    {
    }

    internal override Option<Toy> GetChoice(WishList wishList) => wishList.GetFirstChoice();
}