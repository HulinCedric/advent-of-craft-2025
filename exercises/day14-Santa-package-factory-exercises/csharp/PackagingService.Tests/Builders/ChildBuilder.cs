using Bogus;

namespace PackagingService.Tests.Builders;

public class ChildBuilder
{
    private static readonly Faker Faker = new();

    private int _age;
    private readonly Gift _assignedGift;
    private readonly ChildGender _gender;
    private bool _hasBeenNice;
    private readonly string _name;

    public ChildBuilder()
    {
        _age = Faker.Random.Int(1, 18);
        _name = Faker.Name.FirstName();
        _gender = Faker.Random.Enum<ChildGender>();
        _assignedGift = GiftBuilder.AGift().Build();
        _hasBeenNice = Faker.Random.Bool();
    }

    public static ChildBuilder AChild() => new();

    public ChildBuilder Naughty()
    {
        _hasBeenNice = false;
        return this;
    }

    public ChildBuilder Young() => Aged(3);

    public ChildBuilder NotYoung() => Aged(8);

    public ChildBuilder Aged(int age)
    {
        _age = age;
        return this;
    }

    public Child Build()
        => new(
            name: _name,
            age: _age,
            gender: _gender,
            hasBeenNice: _hasBeenNice,
            assignedGift: _assignedGift);

    public static implicit operator Child(ChildBuilder builder) => builder.Build();
}