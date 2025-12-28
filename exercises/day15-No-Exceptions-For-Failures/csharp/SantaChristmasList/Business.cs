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

    public Result<Sleigh> LoadGiftsInSleigh(params Child[] children)
    {
        var sleigh = new Sleigh();

        foreach (var child in children)
        {
            var gift = _wishList.IdentifyGift(child);
            if (gift is null)
                return new Result<Sleigh>($"No wish found for child: {child.Name}");

            var manufacturedGift = _factory.FindManufacturedGift(gift);
            if (manufacturedGift is null)
                return new Result<Sleigh>($"Gift has not been manufactured: {gift.Name}");

            var finalGift = _inventory.PickUpGift(manufacturedGift.BarCode);
            if (finalGift is null)
                return new Result<Sleigh>($"Gift out of stock: {gift.Name}");

            sleigh.Put(child, $"Gift: {finalGift.Name} has been loaded!");
        }

        return new Result<Sleigh>(sleigh);
    }
}

public class Result<T>
{
    public Result(T success) => Success = success;

    public Result(string failure) => Failure = failure;

    public string? Failure { get; }

    public T? Success { get; }
}