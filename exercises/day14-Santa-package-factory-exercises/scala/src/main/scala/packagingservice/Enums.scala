package packagingservice

sealed trait GiftSize
object GiftSize {
  case object SMALL extends GiftSize
  case object MEDIUM extends GiftSize
  case object LARGE extends GiftSize
  case object EXTRA_LARGE extends GiftSize
}

sealed trait PackageType
object PackageType {
  case object BOX_SMALL extends PackageType
  case object BOX_MEDIUM extends PackageType
  case object BOX_LARGE extends PackageType
  case object GIFT_BAG extends PackageType
  case object WRAPPER_PAPER extends PackageType
  case object SPECIAL_CONTAINER extends PackageType
}

sealed trait ChildGender
object ChildGender {
  case object BOY extends ChildGender
  case object GIRL extends ChildGender
  case object NON_BINARY extends ChildGender
  case object PREFER_NOT_TO_SAY extends ChildGender
}
