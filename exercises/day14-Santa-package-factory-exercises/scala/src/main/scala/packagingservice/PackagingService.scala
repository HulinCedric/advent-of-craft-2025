package packagingservice

import packagingservice.GiftSize._
import packagingservice.PackageType._

class PackagingService {

  def determinePackageType(gift: Gift, child: Child): PackageType = {
    gift.size match {
      case EXTRA_LARGE =>
        SPECIAL_CONTAINER

      case _ if gift.isFragile =>
        gift.size match {
          case SMALL => BOX_SMALL
          case _     => BOX_MEDIUM
        }

      case _ if child.age < 5 =>
        GIFT_BAG

      case SMALL  => BOX_SMALL
      case MEDIUM => BOX_MEDIUM
      case LARGE  => BOX_LARGE
      case _      => WRAPPER_PAPER
    }
  }

  def canPackageGift(gift: Gift, child: Child): Boolean = {
    if (!child.hasBeenNice) {
      false
    } else if (gift.recommendedMinAge > child.age) {
      false
    } else {
      true
    }
  }
}
