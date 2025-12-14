package packagingservice

class PackagingService {

    fun determinePackageType(gift: Gift, child: Child): PackageType {
        if (gift.size == GiftSize.EXTRA_LARGE) {
            return PackageType.SPECIAL_CONTAINER
        }

        if (gift.isFragile) {
            return if (gift.size == GiftSize.SMALL) {
                PackageType.BOX_SMALL
            } else {
                PackageType.BOX_MEDIUM
            }
        }

        if (child.age < 5) {
            return PackageType.GIFT_BAG
        }

        return when (gift.size) {
            GiftSize.SMALL -> PackageType.BOX_SMALL
            GiftSize.MEDIUM -> PackageType.BOX_MEDIUM
            GiftSize.LARGE -> PackageType.BOX_LARGE
            GiftSize.EXTRA_LARGE -> PackageType.WRAPPER_PAPER
        }
    }

    fun canPackageGift(gift: Gift, child: Child): Boolean {
        if (!child.hasBeenNice) {
            return false
        }

        if (gift.recommendedMinAge > child.age) {
            return false
        }

        return true
    }
}
