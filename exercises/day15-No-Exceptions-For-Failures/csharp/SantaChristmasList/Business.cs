using LanguageExt;
using static LanguageExt.Prelude;

namespace SantaChristmasList;

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

    public Either<string, Sleigh> LoadGiftsInSleigh(params Child[] children)
    {
        var sleigh = new Sleigh();

        foreach (var child in children)
        {
            var processResult = ProcessGift(child);

            if (processResult.IsLeft)
                return processResult.LeftToSeq().First();

            processResult.Do(r => LoadGiftInSleigh(sleigh, r));
        }

        return sleigh;
    }

    private static void LoadGiftInSleigh(Sleigh sleigh, (Child child, Gift finalGift) result)
        => sleigh.Put(result.child, $"Gift: {result.finalGift.Name} has been loaded!");

    private Either<string, (Child child, Gift finalGift)> ProcessGift(Child child)
        => from gift in IdentifyGift(child)
            from manufacturedGift in FindManufacturedGift(gift)
            from finalGift in PickUpGift(manufacturedGift)
            select (child, finalGift);

    private Either<string, Gift> IdentifyGift(Child child)
        => Optional(_wishList.IdentifyGift(child))
            .ToEither($"No wish found for child: {child.Name}");

    private Either<string, Gift> FindManufacturedGift(Gift gift)
        => Optional(_factory.FindManufacturedGift(gift))
            .ToEither($"Gift has not been manufactured: {gift.Name}");

    private Either<string, Gift> PickUpGift(Gift manufacturedGift)
        => Optional(_inventory.PickUpGift(manufacturedGift.BarCode))
            .ToEither($"Gift out of stock: {manufacturedGift.Name}");
}