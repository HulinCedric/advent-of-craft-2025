package packagingservice

import packagingservice.GiftSize

final case class Gift(
  name: String,
  size: GiftSize,
  isFragile: Boolean,
  recommendedMinAge: Int
)
