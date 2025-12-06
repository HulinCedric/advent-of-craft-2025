namespace Gifts;

public class Child
{
    private readonly IBehavior? _behavior;
    private readonly List<Toy> _wishlist = new();

    internal Child(string name, IBehavior? behavior)
    {
        Name = name;
        _behavior = behavior;
    }

    public string Name { get; }

    public void SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
    {
        _wishlist.Clear();

        _wishlist.Add(firstChoice);
        _wishlist.Add(secondChoice);
        _wishlist.Add(thirdChoice);
    }

    public Toy? ChooseToy() => _behavior?.ChooseToy(_wishlist);
}