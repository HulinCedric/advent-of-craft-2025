namespace Gifts;

public class Child
{
    private readonly List<Toy> _wishlist = [];

    public Child(string name, IBehavior? behavior)
    {
        Name = name;
        Behavior = behavior;
    }

    public string Name { get; }
    public IBehavior? Behavior { get; }
    public IReadOnlyList<Toy> Wishlist => _wishlist.AsReadOnly();

    public void SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
    {
        _wishlist.Clear();

        _wishlist.Add(firstChoice);
        _wishlist.Add(secondChoice);
        _wishlist.Add(thirdChoice);
    }

    public Toy? ChooseToy() => Behavior?.ChooseToy(Wishlist);
}