using Bogus;

namespace PackagingService.Tests;

public class ChildBuilder
{
    private static readonly Faker Faker = new();

    private int _age;
    private bool _hasBeenNice;

    public ChildBuilder()
    {
        _age = Faker.Random.Int(1, 18);
        _hasBeenNice = Faker.Random.Bool();
    }

    public static ChildBuilder AChild() => new();

    public ChildBuilder Naughty()
    {
        _hasBeenNice = false;
        return this;
    }

    public ChildBuilder Young() => Aged(3);

    public ChildBuilder Aged(int age)
    {
        _age = age;
        return this;
    }

    public Child Build()
        => new(
            name: "Bobby",
            age: _age,
            gender: ChildGender.BOY,
            hasBeenNice: _hasBeenNice,
            assignedGift: GiftBuilder.AGift().Build());

    public static implicit operator Child(ChildBuilder builder) => builder.Build();
}