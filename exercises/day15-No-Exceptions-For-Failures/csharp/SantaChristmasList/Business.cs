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

    public Error<Sleigh> LoadGiftsInSleigh(Child child)
    {
        try
        {
            Child[] children = new[] { child };
            var sleigh = new Sleigh();

            foreach (var child1 in children)
            {
                try
                {
                    var gift = _wishList.IdentifyGift(child1);
                    if (gift is null)
                        throw new ChildWishNotFoundException(child1);

                    var manufacturedGift = _factory.FindManufacturedGift(gift);
                    if (manufacturedGift is null)
                        throw new GiftNotManufacturedException(gift);

                    var finalGift = _inventory.PickUpGift(manufacturedGift.BarCode);
                    if (finalGift is null)
                        throw new GiftOutOfStockException(manufacturedGift);

                    sleigh.Put(child1, $"Gift: {finalGift.Name} has been loaded!");
                }
                catch (Exception e) when (e is ChildWishNotFoundException
                                              or GiftNotManufacturedException
                                              or GiftOutOfStockException)
                {
                    throw new BusinessException("Unexpected error while loading sleigh", e);
                }
            }

            return new Error<Sleigh>(sleigh);
        }
        catch (BusinessException ex) when (ex.InnerException is not null)
        {
            return new Error<Sleigh>(ex.InnerException.Message);
        }
        catch (Exception ex)
        {
            return new Error<Sleigh>(ex.Message);
        }
    }
}

public class Error<T>
{
    public Error(T success) => Success = success;

    public Error(string failure) => Failure = failure;

    public string? Failure { get; }

    public T? Success { get; }
}