import { Gift } from "./Gift";
import { Child } from "./Child";
import { GiftSize, PackageType } from "./enums";

export class PackagingService {
  determinePackageType(gift: Gift, child: Child): PackageType {
    if (gift.size === GiftSize.EXTRA_LARGE) {
      return PackageType.SPECIAL_CONTAINER;
    }

    if (gift.isFragile) {
      return gift.size === GiftSize.SMALL
        ? PackageType.BOX_SMALL
        : PackageType.BOX_MEDIUM;
    }

    if (child.age < 5) {
      return PackageType.GIFT_BAG;
    }

    switch (gift.size) {
      case GiftSize.SMALL:
        return PackageType.BOX_SMALL;
      case GiftSize.MEDIUM:
        return PackageType.BOX_MEDIUM;
      case GiftSize.LARGE:
        return PackageType.BOX_LARGE;
      default:
        return PackageType.WRAPPER_PAPER;
    }
  }

  canPackageGift(gift: Gift, child: Child): boolean {
    if (!child.hasBeenNice) {
      return false;
    }

    if (gift.recommendedMinAge > child.age) {
      return false;
    }

    return true;
  }
}
