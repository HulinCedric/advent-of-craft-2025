namespace PackagingService.Tests;

public class ChildBuilder
{
    private int _age = 7;
    private bool _hasBeenNice;

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
}