using FluentAssertions;
using LanguageExt.Common;
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

        var result = business.LoadGiftsInSleigh(timmy);

        result.Failures.Should().BeEquivalentTo([Error.New("No wish found for child: Timmy")]);
        result.Sleigh.Messages.Should().BeEmpty();
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

        var result = business.LoadGiftsInSleigh(timmy);

        result.Failures.Should().BeEquivalentTo([Error.New("Gift has not been manufactured: Lego Death Star")]);
        result.Sleigh.Messages.Should().BeEmpty();
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

        var result = business.LoadGiftsInSleigh(timmy);

        result.Failures.Should().BeEquivalentTo([Error.New("Gift out of stock: Red Bike")]);
        result.Sleigh.Messages.Should().BeEmpty();
    }

    [Fact]
    public void Load_one_wished_gift_in_sleigh()
    {
        var timmy = new Child("Timmy");
        var wishedGift = new Gift("Red Bike", "BARCODE-456");
        var manufacturedGift = new Gift("Red Bike", "BARCODE-456");
        var inventoriedGift = new Gift("Red Bike", "BARCODE-456");

        IWishList wishList = new StubWishList(child => wishedGift);
        IFactory factory = new StubFactory(gift => manufacturedGift);
        IInventory inventory = new StubInventory(barCode => inventoriedGift);

        var business = new Business(factory, inventory, wishList);

        var result = business.LoadGiftsInSleigh(timmy);

        result.Failures.Should().BeEmpty();
        result.Sleigh.Messages[timmy].Should().Be("Gift: Red Bike has been loaded!");
    }

    [Fact]
    public void Load_wished_gift_in_sleigh_and_return_failure()
    {
        var timmy = new Child("Timmy");
        var eloise = new Child("Eloise");
        var wishedGift = new Gift("Red Bike", "BARCODE-456");
        var manufacturedGift = new Gift("Red Bike", "BARCODE-456");
        var inventoriedGift = new Gift("Red Bike", "BARCODE-456");

        IWishList wishList = new StubWishList(child => child == eloise ? wishedGift : null);
        IFactory factory = new StubFactory(gift => manufacturedGift);
        IInventory inventory = new StubInventory(barCode => inventoriedGift);

        var business = new Business(factory, inventory, wishList);

        var result = business.LoadGiftsInSleigh(timmy, eloise);

        result.Failures.Should().BeEquivalentTo([Error.New("No wish found for child: Timmy")]);
        result.Sleigh.Messages[eloise].Should().Be("Gift: Red Bike has been loaded!");
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