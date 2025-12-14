using Xunit;

namespace PackagingService.Tests;

public class PackagingServiceTests
{
    private readonly PackagingService _service;
        
    public PackagingServiceTests()
    {
        _service = new PackagingService();
    }
        
    [Fact]
    public void ShouldUseSmallBoxForSmallNonFragileGift()
    {
        // Arrange
        var gift = GiftBuilder.AGift().Build();
            
        var child = new Child(
            name: "Tommy",
            age: 8,
            gender: ChildGender.BOY,
            hasBeenNice: true,
            assignedGift: gift
        );
            
        // Act
        var result = _service.DeterminePackageType(gift, child);
            
        // Assert
        Assert.Equal(PackageType.BOX_SMALL, result);
    }

    [Fact]
    public void ShouldUseSpecialContainerForExtraLargeGift()
    {
        // Arrange
        var gift = new Gift(
            name: "Bicycle",
            size: GiftSize.EXTRA_LARGE,
            isFragile: false,
            recommendedMinAge: 8
        );
            
        var child = new Child(
            name: "Sarah",
            age: 10,
            gender: ChildGender.GIRL,
            hasBeenNice: true,
            assignedGift: gift
        );
            
        // Act
        var result = _service.DeterminePackageType(gift, child);
            
        // Assert
        Assert.Equal(PackageType.SPECIAL_CONTAINER, result);
    }
        
    [Fact]
    public void ShouldUseGiftBagForYoungChildren()
    {
        // Arrange
        var gift = new Gift(
            name: "Teddy Bear",
            size: GiftSize.MEDIUM,
            isFragile: false,
            recommendedMinAge: 1
        );
            
        var child = new Child(
            name: "Emma",
            age: 3,
            gender: ChildGender.GIRL,
            hasBeenNice: true,
            assignedGift: gift
        );
            
        // Act
        var result = _service.DeterminePackageType(gift, child);
            
        // Assert
        Assert.Equal(PackageType.GIFT_BAG, result);
    }
        
    [Fact]
    public void ShouldNotPackageGiftForNaughtyChild()
    {
        // Arrange
        var gift = new Gift(
            name: "Video Game Console",
            size: GiftSize.MEDIUM,
            isFragile: false,
            recommendedMinAge: 6
        );
            
        var child = new Child(
            name: "Bobby",
            age: 7,
            gender: ChildGender.BOY,
            hasBeenNice: false, // naughty!
            assignedGift: gift
        );
            
        // Act
        var result = _service.CanPackageGift(gift, child);
            
        // Assert
        Assert.False(result);
    }
        
    [Fact]
    public void ShouldNotPackageGiftForChildTooYoung()
    {
        // Arrange
        var gift = new Gift(
            name: "Complex Building Set",
            size: GiftSize.LARGE,
            isFragile: false,
            recommendedMinAge: 8 // too old for this child
        );
            
        var child = new Child(
            name: "Lily",
            age: 4,
            gender: ChildGender.GIRL,
            hasBeenNice: true,
            assignedGift: gift
        );
            
        // Act
        var result = _service.CanPackageGift(gift, child);
            
        // Assert
        Assert.False(result);
    }
}

public class GiftBuilder
{
    public static GiftBuilder AGift() => new();

    public Gift Build()
    {
        var gift = new Gift(
            name: "Action Figure",
            size: GiftSize.SMALL,
            isFragile: false,
            recommendedMinAge: 5
        );
        return gift;
    }
}