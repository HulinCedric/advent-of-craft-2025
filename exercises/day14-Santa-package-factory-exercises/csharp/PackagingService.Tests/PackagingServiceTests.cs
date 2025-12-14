using FluentAssertions;
using Xunit;
using static PackagingService.PackageType;
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
        Assert.Equal(BOX_SMALL, result);
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
        Assert.Equal(SPECIAL_CONTAINER, result);
    }

    [Fact]
    public void ShouldUseGiftBagForYoungChildren()
        => _service.DeterminePackageType(
                AGift(),
                AChild().Young())
            .Should()
            .Be(GIFT_BAG);

    [Fact]
    public void ShouldNotPackageGiftForNaughtyChild()
        => _service.CanPackageGift(
                AGift(),
                AChild().Naughty())
            .Should()
            .BeFalse();

    [Fact]
    public void ShouldNotPackageGiftForChildTooYoung()
        => _service.CanPackageGift(
                AGift().RecommendedForAgesAndUp(8),
                AChild().Aged(4))
            .Should()
            .BeFalse();
}