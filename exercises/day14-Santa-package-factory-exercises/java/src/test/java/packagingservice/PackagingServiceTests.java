package packagingservice;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

public class PackagingServiceTests {

    private final PackagingService service = new PackagingService();

    @Test
    public void shouldUseSmallBoxForSmallNonFragileGift() {
        Gift gift = new Gift(
            "Action Figure",
            GiftSize.SMALL,
            false,
            5
        );

        Child child = new Child(
            "Tommy",
            8,
            ChildGender.BOY,
            true,
            gift
        );

        PackageType result = service.determinePackageType(gift, child);

        assertEquals(PackageType.BOX_SMALL, result);
    }

    @Test
    public void shouldUseSpecialContainerForExtraLargeGift() {
        Gift gift = new Gift(
            "Bicycle",
            GiftSize.EXTRA_LARGE,
            false,
            8
        );

        Child child = new Child(
            "Sarah",
            10,
            ChildGender.GIRL,
            true,
            gift
        );

        PackageType result = service.determinePackageType(gift, child);

        assertEquals(PackageType.SPECIAL_CONTAINER, result);
    }

    @Test
    public void shouldUseGiftBagForYoungChildren() {
        Gift gift = new Gift(
            "Teddy Bear",
            GiftSize.MEDIUM,
            false,
            1
        );

        Child child = new Child(
            "Emma",
            3,
            ChildGender.GIRL,
            true,
            gift
        );

        PackageType result = service.determinePackageType(gift, child);

        assertEquals(PackageType.GIFT_BAG, result);
    }

    @Test
    public void shouldNotPackageGiftForNaughtyChild() {
        Gift gift = new Gift(
            "Video Game Console",
            GiftSize.MEDIUM,
            false,
            6
        );

        Child child = new Child(
            "Bobby",
            7,
            ChildGender.BOY,
            false,
            gift
        );

        boolean result = service.canPackageGift(gift, child);

        assertFalse(result);
    }

    @Test
    public void shouldNotPackageGiftForChildTooYoung() {
        Gift gift = new Gift(
            "Complex Building Set",
            GiftSize.LARGE,
            false,
            8
        );

        Child child = new Child(
            "Lily",
            4,
            ChildGender.GIRL,
            true,
            gift
        );

        boolean result = service.canPackageGift(gift, child);

        assertFalse(result);
    }
}
