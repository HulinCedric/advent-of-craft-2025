namespace PackagingService
{
    public class Gift
    {
        public string Name { get; }
        public GiftSize Size { get; }
        public bool IsFragile { get; }
        public int RecommendedMinAge { get; }
        
        public Gift(string name, GiftSize size, bool isFragile, int recommendedMinAge)
        {
            Name = name;
            Size = size;
            IsFragile = isFragile;
            RecommendedMinAge = recommendedMinAge;
        }
    }
}
