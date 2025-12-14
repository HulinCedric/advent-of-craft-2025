package packagingservice

import packagingservice.{ChildGender, Gift}

final case class Child(
  name: String,
  age: Int,
  gender: ChildGender,
  hasBeenNice: Boolean,
  assignedGift: Gift
)
