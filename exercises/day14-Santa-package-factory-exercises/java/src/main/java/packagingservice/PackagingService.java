package packagingservice;

public class PackagingService {

    public PackageType determinePackageType(Gift gift, Child child) {
        if (gift.getSize() == GiftSize.EXTRA_LARGE) {
            return PackageType.SPECIAL_CONTAINER;
        }

        if (gift.isFragile()) {
            return gift.getSize() == GiftSize.SMALL
                    ? PackageType.BOX_SMALL
                    : PackageType.BOX_MEDIUM;
        }

        if (child.getAge() < 5) {
            return PackageType.GIFT_BAG;
        }

        switch (gift.getSize()) {
            case SMALL:
                return PackageType.BOX_SMALL;
            case MEDIUM:
                return PackageType.BOX_MEDIUM;
            case LARGE:
                return PackageType.BOX_LARGE;
            default:
                return PackageType.WRAPPER_PAPER;
        }
    }

    public boolean canPackageGift(Gift gift, Child child) {
        if (!child.hasBeenNice()) {
            return false;
        }

        if (gift.getRecommendedMinAge() > child.getAge()) {
            return false;
        }

        return true;
    }
}
