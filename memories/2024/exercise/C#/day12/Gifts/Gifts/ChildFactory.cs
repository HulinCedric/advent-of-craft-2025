namespace Gifts;

public sealed class ChildFactory
{
    private readonly IBehaviorFactoryResolver _resolver;

    public ChildFactory(IBehaviorFactoryResolver resolver)
    {
        _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
    }

    public Child Create(string name, string behaviorKey)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(behaviorKey)) throw new ArgumentNullException(nameof(behaviorKey));

        var factory = _resolver.GetFactory(behaviorKey);
        if (factory == null) throw new ArgumentException($"Unknown behavior key: '{behaviorKey}'", nameof(behaviorKey));

        var behavior = factory.Create() ?? throw new InvalidOperationException("Behavior factory returned null");
        return new Child(name, behavior);
    }

    public Child CreateWithWishlist(string name, string behaviorKey, Toy firstChoice, Toy secondChoice, Toy thirdChoice)
    {
        var child = Create(name, behaviorKey);
        child.SetWishList(firstChoice, secondChoice, thirdChoice);
        return child;
    }
}