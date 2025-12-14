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

        var child = AChild().Build();

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

        var child = AChild().Build();

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