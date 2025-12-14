package packagingservice

import org.scalatest.funsuite.AnyFunSuite

class PackagingServiceTests extends AnyFunSuite {

  private val service = new PackagingService

  test("should use small box for small non fragile gift") {
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

    assert(result == PackageType.BOX_SMALL)
  }

  test("should use special container for extra large gift") {
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

    assert(result == PackageType.SPECIAL_CONTAINER)
  }

  test("should use gift bag for young children") {
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

    assert(result == PackageType.GIFT_BAG)
  }

  test("should not package gift for naughty child") {
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

    assert(!result)
  }

  test("should not package gift for child too young") {
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

    assert(!result)
  }
}
