namespace PackagingService
{
    public class PackagingService
    {
        public PackageType DeterminePackageType(Gift gift, Child child)
        {
            if (gift.Size == GiftSize.EXTRA_LARGE)
            {
                return PackageType.SPECIAL_CONTAINER;
            }
            
            if (gift.IsFragile)
            {
                return gift.Size == GiftSize.SMALL 
                    ? PackageType.BOX_SMALL 
                    : PackageType.BOX_MEDIUM;
            }
            
            if (child.Age < 5)
            {
                return PackageType.GIFT_BAG;
            }
            
            return gift.Size switch
            {
                GiftSize.SMALL => PackageType.BOX_SMALL,
                GiftSize.MEDIUM => PackageType.BOX_MEDIUM,
                GiftSize.LARGE => PackageType.BOX_LARGE,
                _ => PackageType.WRAPPER_PAPER
            };
        }
        
        public bool CanPackageGift(Gift gift, Child child)
        {
            if (!child.HasBeenNice)
            {
                return false;
            }
            
            if (gift.RecommendedMinAge > child.Age)
            {
                return false;
            }
            
            return true;
        }
    }
}
