package packagingservice

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

class PackagingServiceTests {

    private val service = PackagingService()

    @Test
    fun shouldUseSmallBoxForSmallNonFragileGift() {
        val gift = Gift(
            name = "Action Figure",
            size = GiftSize.SMALL,
            isFragile = false,
            recommendedMinAge = 5
        )

        val child = Child(
            name = "Tommy",
            age = 8,
            gender = ChildGender.BOY,
            hasBeenNice = true,
            assignedGift = gift
        )

        val result = service.determinePackageType(gift, child)

        assertEquals(PackageType.BOX_SMALL, result)
    }

    @Test
    fun shouldUseSpecialContainerForExtraLargeGift() {
        val gift = Gift(
            name = "Bicycle",
            size = GiftSize.EXTRA_LARGE,
            isFragile = false,
            recommendedMinAge = 8
        )

        val child = Child(
            name = "Sarah",
            age = 10,
            gender = ChildGender.GIRL,
            hasBeenNice = true,
            assignedGift = gift
        )

        val result = service.determinePackageType(gift, child)

        assertEquals(PackageType.SPECIAL_CONTAINER, result)
    }

    @Test
    fun shouldUseGiftBagForYoungChildren() {
        val gift = Gift(
            name = "Teddy Bear",
            size = GiftSize.MEDIUM,
            isFragile = false,
            recommendedMinAge = 1
        )

        val child = Child(
            name = "Emma",
            age = 3,
            gender = ChildGender.GIRL,
            hasBeenNice = true,
            assignedGift = gift
        )

        val result = service.determinePackageType(gift, child)

        assertEquals(PackageType.GIFT_BAG, result)
    }

    @Test
    fun shouldNotPackageGiftForNaughtyChild() {
        val gift = Gift(
            name = "Video Game Console",
            size = GiftSize.MEDIUM,
            isFragile = false,
            recommendedMinAge = 6
        )

        val child = Child(
            name = "Bobby",
            age = 7,
            gender = ChildGender.BOY,
            hasBeenNice = false,
            assignedGift = gift
        )

        val result = service.canPackageGift(gift, child)

        assertFalse(result)
    }

    @Test
    fun shouldNotPackageGiftForChildTooYoung() {
        val gift = Gift(
            name = "Complex Building Set",
            size = GiftSize.LARGE,
            isFragile = false,
            recommendedMinAge = 8
        )

        val child = Child(
            name = "Lily",
            age = 4,
            gender = ChildGender.GIRL,
            hasBeenNice = true,
            assignedGift = gift
        )

        val result = service.canPackageGift(gift, child)

        assertFalse(result)
    }
}
