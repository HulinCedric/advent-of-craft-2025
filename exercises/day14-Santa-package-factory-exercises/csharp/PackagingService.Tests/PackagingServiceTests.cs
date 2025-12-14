using FluentAssertions;
using Xunit;
using static PackagingService.PackageType;
using static PackagingService.Tests.ChildBuilder;
using static PackagingService.Tests.GiftBuilder;

namespace PackagingService.Tests;

public class PackagingServiceTests
{
    private readonly PackagingService _service = new();

    [Fact]
    public void ShouldUseSmallBoxForSmallNonFragileGift()
        => _service.DeterminePackageType(
                AGift().Small().NonFragile(),
                AChild())
            .Should()
            .Be(BOX_SMALL);

    [Fact]
    public void ShouldUseSpecialContainerForExtraLargeGift()
        => _service.DeterminePackageType(
                AGift().ExtraLarge(),
                AChild())
            .Should()
            .Be(SPECIAL_CONTAINER);

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