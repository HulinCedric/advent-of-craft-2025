using Xunit;
using static PackagingService.Tests.ChildBuilder;
using static PackagingService.Tests.GiftBuilder;

namespace PackagingService.Tests;

public class PackagingServiceTests
{
    private readonly PackagingService _service;

    public PackagingServiceTests() => _service = new PackagingService();

    [Fact]
    public void ShouldUseSmallBoxForSmallNonFragileGift()
    {
        // Arrange
        var gift = AGift().Small().NonFragile().Build();

        var child = new Child(
            name: "Tommy",
            age: 8,
            gender: ChildGender.BOY,
            hasBeenNice: true,
            assignedGift: gift);

        // Act
        var result = _service.DeterminePackageType(gift, child);

        // Assert
        Assert.Equal(PackageType.BOX_SMALL, result);
    }

    [Fact]
    public void ShouldUseSpecialContainerForExtraLargeGift()
    {
        // Arrange
        var gift = AGift().ExtraLarge().Build();

        var child = new Child(
            name: "Sarah",
            age: 10,
            gender: ChildGender.GIRL,
            hasBeenNice: true,
            assignedGift: gift);

        // Act
        var result = _service.DeterminePackageType(gift, child);

        // Assert
        Assert.Equal(PackageType.SPECIAL_CONTAINER, result);
    }

    [Fact]
    public void ShouldUseGiftBagForYoungChildren()
    {
        // Arrange
        var gift = AGift().Build();

        var child = AChild().Young().Build();

        // Act
        var result = _service.DeterminePackageType(gift, child);

        // Assert
        Assert.Equal(PackageType.GIFT_BAG, result);
    }

    [Fact]
    public void ShouldNotPackageGiftForNaughtyChild()
    {
        // Arrange
        var gift = AGift().Build();

        var child = AChild().Naughty().Build();

        // Act
        var result = _service.CanPackageGift(gift, child);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ShouldNotPackageGiftForChildTooYoung()
    {
        // Arrange
        var gift = AGift().RecommendedForAgesAndUp(8).Build();

        var child = AChild().Aged(4).Build();

        // Act
        var result = _service.CanPackageGift(gift, child);

        // Assert
        Assert.False(result);
    }
}

public class ChildBuilder
{
    private int _age = 7;
    private bool _hasBeenNice;

    public static ChildBuilder AChild() => new();

    public ChildBuilder Naughty()
    {
        _hasBeenNice = false;
        return this;
    }

    public ChildBuilder Young() => Aged(3);

    public ChildBuilder Aged(int age)
    {
        _age = age;
        return this;
    }

    public Child Build()
        => new(
            name: "Bobby",
            age: _age,
            gender: ChildGender.BOY,
            hasBeenNice: _hasBeenNice,
            assignedGift: AGift().Build());
}