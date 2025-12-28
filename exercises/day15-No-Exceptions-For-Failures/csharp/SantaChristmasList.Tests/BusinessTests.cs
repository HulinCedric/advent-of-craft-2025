using Xunit;

namespace SantaChristmasList.Tests;

public class BusinessTests
{
    [Fact]
    public void Child_wish_not_found_error_message_is_extracted()
    {
        var timmy = new Child("Timmy");

        IWishList wishList = new StubWishList(_ => null);
        IFactory factory = new StubFactory(_ => null);
        IInventory inventory = new StubInventory(_ => null);

        var business = new Business(factory, inventory, wishList);

        var failure = business.LoadGiftsInSleigh(timmy).Failure;

        Assert.Equal("No wish found for child: Timmy", failure);
    }

    [Fact]
    public void Gift_not_manufactured_error_message_is_extracted()
    {
        var timmy = new Child("Timmy");
        var wishedGift = new Gift("Lego Death Star", "BARCODE-123");

        IWishList wishList = new StubWishList(child => wishedGift);
        IFactory factory = new StubFactory(_ => null);
        IInventory inventory = new StubInventory(_ => null);

        var business = new Business(factory, inventory, wishList);

        var failure = business.LoadGiftsInSleigh(timmy).Failure;

        Assert.Equal("Gift has not been manufactured: Lego Death Star", failure);
    }

    [Fact]
    public void Gift_out_of_stock_error_message_is_extracted()
    {
        var timmy = new Child("Timmy");
        var wishedGift = new Gift("Red Bike", "BARCODE-456");
        var manufacturedGift = new Gift("Red Bike", "BARCODE-456");

        IWishList wishList = new StubWishList(child => wishedGift);
        IFactory factory = new StubFactory(gift => manufacturedGift);
        IInventory inventory = new StubInventory(_ => null);

        var business = new Business(factory, inventory, wishList);

        var failure = business.LoadGiftsInSleigh(timmy).Failure;

        Assert.Equal("Gift out of stock: Red Bike", failure);
    }

    private static string LoadGiftAndExtractInnerErrorMessage(Business business, Child child)
    {
        try
        {
            business.LoadGiftsInSleighUnsafe(child);
        }
        catch (BusinessException ex)
        {
            if (ex.InnerException == null)
                throw;

            return ex.InnerException.Message!;
        }

        throw new Exception("Expected BusinessException to be thrown");
    }

    private class StubWishList : IWishList
    {
        private readonly Func<Child, Gift?> _fn;
        public StubWishList(Func<Child, Gift?> fn) => _fn = fn;
        public Gift? IdentifyGift(Child child) => _fn(child);
    }

    private class StubFactory : IFactory
    {
        private readonly Func<Gift, Gift?> _fn;
        public StubFactory(Func<Gift, Gift?> fn) => _fn = fn;
        public Gift? FindManufacturedGift(Gift gift) => _fn(gift);
    }

    private class StubInventory : IInventory
    {
        private readonly Func<string, Gift?> _fn;
        public StubInventory(Func<string, Gift?> fn) => _fn = fn;
        public Gift? PickUpGift(string barCode) => _fn(barCode);
    }
}