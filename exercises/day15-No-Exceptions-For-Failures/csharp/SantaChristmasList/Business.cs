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
            var children = new[] { child };
            var sleigh = new Sleigh();

            foreach (var child1 in children)
            {
                try
                {
                    var gift = _wishList.IdentifyGift(child1);
                    if (gift is null)
                        return new Error<Sleigh>($"No wish found for child: {child.Name}");

                    var manufacturedGift = _factory.FindManufacturedGift(gift);
                    if (manufacturedGift is null)
                        return new Error<Sleigh>($"Gift has not been manufactured: {gift.Name}");

                    var finalGift = _inventory.PickUpGift(manufacturedGift.BarCode);
                    if (finalGift is null)
                        return new Error<Sleigh>($"Gift out of stock: {gift.Name}");

                    sleigh.Put(child1, $"Gift: {finalGift.Name} has been loaded!");
                }
                catch (Exception e)
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