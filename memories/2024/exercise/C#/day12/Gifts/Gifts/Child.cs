namespace Gifts;

public class Child
{
    public string Name { get; }
    public IBehavior Behavior { get; }

    private readonly List<Toy> _wishlist = new();

    public Child(string name, IBehavior behavior)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
    }

    public void SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
    {
        if (firstChoice is null) throw new ArgumentNullException(nameof(firstChoice));
        if (secondChoice is null) throw new ArgumentNullException(nameof(secondChoice));
        if (thirdChoice is null) throw new ArgumentNullException(nameof(thirdChoice));

        _wishlist.Clear();
        _wishlist.Add(firstChoice);
        _wishlist.Add(secondChoice);
        _wishlist.Add(thirdChoice);
    }

    public Toy? ChooseToy() => Behavior.ChooseToy(_wishlist);
}