using LanguageExt;

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
            var identificationResult = IdentifyGift(child);
            if (identificationResult.IsLeft)
                return identificationResult.LeftToSeq().First();

            var gift = identificationResult.RightToSeq().First();

            var manufacturedGiftResult = FindManufacturedGift(gift);
            if (manufacturedGiftResult.IsLeft)
                return manufacturedGiftResult.LeftToSeq().First();

            var manufacturedGift = manufacturedGiftResult.RightToSeq().First();

            var finalGift = _inventory.PickUpGift(manufacturedGift.BarCode);
            if (finalGift is null)
                return $"Gift out of stock: {gift.Name}";

            sleigh.Put(child, $"Gift: {finalGift.Name} has been loaded!");
        }

        return sleigh;
    }

    private Either<string, Gift> FindManufacturedGift(Gift gift)
    {
        var manufacturedGift = _factory.FindManufacturedGift(gift);
        if (manufacturedGift is null) return $"Gift has not been manufactured: {gift.Name}";

        return manufacturedGift;
    }

    private Either<string, Gift> IdentifyGift(Child child)
    {
        var gift = _wishList.IdentifyGift(child);
        if (gift is null) return $"No wish found for child: {child.Name}";

        return gift;
    }
}