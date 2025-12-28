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

    public Sleigh LoadGiftsInSleighUnsafe(params Child[] children)
    {
        var sleigh = new Sleigh();

        foreach (var child in children)
        {
            try
            {
                LoadGiftForChild(sleigh, child);
            }
            catch (Exception e) when (e is ChildWishNotFoundException
                                          or GiftNotManufacturedException
                                          or GiftOutOfStockException)
            {
                throw new BusinessException("Unexpected error while loading sleigh", e);
            }
        }

        return sleigh;
    }

    private void LoadGiftForChild(Sleigh sleigh, Child child)
    {
        var gift = _wishList.IdentifyGift(child);
        if (gift is null)
            throw new ChildWishNotFoundException(child);

        var manufacturedGift = _factory.FindManufacturedGift(gift);
        if (manufacturedGift is null)
            throw new GiftNotManufacturedException(gift);

        var finalGift = _inventory.PickUpGift(manufacturedGift.BarCode);
        if (finalGift is null)
            throw new GiftOutOfStockException(manufacturedGift);

        sleigh.Put(child, $"Gift: {finalGift.Name} has been loaded!");
    }
}