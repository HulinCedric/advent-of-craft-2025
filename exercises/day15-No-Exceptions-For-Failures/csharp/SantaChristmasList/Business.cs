using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace SantaChristmasList;

using GiftProcessResult = (Child child, Gift gift);

public class Business
{
    private readonly IFactory _factory;
    private readonly IInventory _inventory;
    private readonly IWishList _wishList;

    public Business(IFactory factory, IInventory inventory, IWishList wishList)
    {
        _factory = factory;
        _inventory = inventory;
        _wishList = wishList;
    }

    public Either<Error, Sleigh> LoadGiftsInSleigh(params Child[] children)
        => ProcessGiftsFor(children)
            .Map(gifts => gifts.Fold(new Sleigh(), LoadGiftInSleigh));

    private Either<Error, IEnumerable<GiftProcessResult>> ProcessGiftsFor(Child[] children)
        => children.Map(ProcessGift).Sequence();

    private Either<Error, GiftProcessResult> ProcessGift(Child child)
        => from gift in IdentifyGift(child)
            from manufacturedGift in FindManufacturedGift(gift)
            from finalGift in PickUpGift(manufacturedGift)
            select (child, finalGift);

    private Either<Error, Gift> IdentifyGift(Child child)
        => Optional(_wishList.IdentifyGift(child))
            .ToEither<Error>($"No wish found for child: {child.Name}");

    private Either<Error, Gift> FindManufacturedGift(Gift gift)
        => Optional(_factory.FindManufacturedGift(gift))
            .ToEither<Error>($"Gift has not been manufactured: {gift.Name}");

    private Either<Error, Gift> PickUpGift(Gift manufacturedGift)
        => Optional(_inventory.PickUpGift(manufacturedGift.BarCode))
            .ToEither<Error>($"Gift out of stock: {manufacturedGift.Name}");

    private static Sleigh LoadGiftInSleigh(Sleigh sleigh, GiftProcessResult result)
    {
        sleigh.Put(result.child, $"Gift: {result.gift.Name} has been loaded!");
        return sleigh;
    }
}