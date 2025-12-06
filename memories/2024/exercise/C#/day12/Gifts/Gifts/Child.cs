namespace Gifts;

public class Child
{
    public string Name { get; }
    public IBehavior Behavior { get; }

    private readonly List<Toy> _wishlist = new();
    public IReadOnlyList<Toy> Wishlist => _wishlist.AsReadOnly();

    // Primary ctor: accept an IBehavior
    public Child(string name, IBehavior behavior)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
    }

    // Backwards-compatible ctor: accept a behavior key string and resolve it to an IBehavior
    public Child(string name, string behaviorKey)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(behaviorKey)) throw new ArgumentNullException(nameof(behaviorKey));

        var factory = BehaviorFactoryProducer.GetFactory(behaviorKey);
        if (factory == null)
            throw new ArgumentException($"Unknown behavior key: '{behaviorKey}'", nameof(behaviorKey));

        Behavior = factory.Create();
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
}