package packagingservice;

public class Gift {

    private final String name;
    private final GiftSize size;
    private final boolean fragile;
    private final int recommendedMinAge;

    public Gift(String name, GiftSize size, boolean isFragile, int recommendedMinAge) {
        this.name = name;
        this.size = size;
        this.fragile = isFragile;
        this.recommendedMinAge = recommendedMinAge;
    }

    public String getName() {
        return name;
    }

    public GiftSize getSize() {
        return size;
    }

    public boolean isFragile() {
        return fragile;
    }

    public int getRecommendedMinAge() {
        return recommendedMinAge;
    }
}
