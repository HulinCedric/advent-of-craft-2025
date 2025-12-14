import { PackagingService } from "./PackagingService";
import { Gift } from "./Gift";
import { Child } from "./Child";
import { ChildGender, GiftSize, PackageType } from "./enums";

describe("PackagingService", () => {
  const service = new PackagingService();

  it("should use small box for small non-fragile gift", () => {
    const gift = new Gift("Action Figure", GiftSize.SMALL, false, 5);
    const child = new Child("Tommy", 8, ChildGender.BOY, true, gift);

    const result = service.determinePackageType(gift, child);

    expect(result).toBe(PackageType.BOX_SMALL);
  });

  it("should use special container for extra large gift", () => {
    const gift = new Gift("Bicycle", GiftSize.EXTRA_LARGE, false, 8);
    const child = new Child("Sarah", 10, ChildGender.GIRL, true, gift);

    const result = service.determinePackageType(gift, child);

    expect(result).toBe(PackageType.SPECIAL_CONTAINER);
  });

  it("should use gift bag for young children", () => {
    const gift = new Gift("Teddy Bear", GiftSize.MEDIUM, false, 1);
    const child = new Child("Emma", 3, ChildGender.GIRL, true, gift);

    const result = service.determinePackageType(gift, child);

    expect(result).toBe(PackageType.GIFT_BAG);
  });

  it("should not package gift for naughty child", () => {
    const gift = new Gift("Video Game Console", GiftSize.MEDIUM, false, 6);
    const child = new Child("Bobby", 7, ChildGender.BOY, false, gift);

    const result = service.canPackageGift(gift, child);

    expect(result).toBe(false);
  });

  it("should not package gift for child too young", () => {
    const gift = new Gift("Complex Building Set", GiftSize.LARGE, false, 8);
    const child = new Child("Lily", 4, ChildGender.GIRL, true, gift);

    const result = service.canPackageGift(gift, child);

    expect(result).toBe(false);
  });
});
