using Bogus;

namespace PackagingService.Tests;

public class GiftBuilder
{
    private static readonly Faker Faker = new();

    private bool _isFragile;
    private int _recommendedMinAge;
    private string _name;
    private GiftSize _size;

    public GiftBuilder()
    {
        _isFragile = Faker.Random.Bool();
        _recommendedMinAge = Faker.Random.Int(1, 18);
        _name = Faker.Commerce.ProductName();
        _size = Faker.Random.Enum<GiftSize>();
    }

    public static GiftBuilder AGift() => new();

    public GiftBuilder Small()
    {
        _size = GiftSize.SMALL;
        return this;
    }

    public GiftBuilder ExtraLarge()
    {
        _size = GiftSize.EXTRA_LARGE;
        return this;
    }

    public GiftBuilder NonFragile()
    {
        _isFragile = false;
        return this;
    }

    public GiftBuilder RecommendedForAgesAndUp(int age)
    {
        _recommendedMinAge = age;
        return this;
    }

    public Gift Build()
        => new(
            name: _name,
            size: _size,
            isFragile: _isFragile,
            recommendedMinAge: _recommendedMinAge);

    public static implicit operator Gift(GiftBuilder builder) => builder.Build();
}