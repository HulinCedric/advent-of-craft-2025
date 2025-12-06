namespace Gifts;

public static class ChildFactory
{
    /// <summary>
    /// Create a Child with the given name and behavior key (string). The behavior key is validated via BehaviorFactoryProducer.
    /// </summary>
    public static Child Create(string name, string behaviorKey)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(behaviorKey)) throw new ArgumentNullException(nameof(behaviorKey));

        var factory = BehaviorFactoryProducer.GetFactory(behaviorKey);
        if (factory == null)
            throw new ArgumentException($"Unknown behavior key: '{behaviorKey}'", nameof(behaviorKey));

        var behavior = factory.Create();
        return new Child(name, behavior);
    }

    /// <summary>
    /// Create a Child with the given name and behavior key (string), using the provided resolver for factory resolution.
    /// </summary>
    public static Child Create(string name, string behaviorKey, IBehaviorFactory behaviorFactory)
    {
        if (behaviorFactory == null) throw new ArgumentNullException(nameof(behaviorFactory));
        BehaviorFactoryProducer.GetFactoryOrThrow(behaviorKey);
        return Create(name, behaviorKey);
    }

    /// <summary>
    /// Create a Child and set its wishlist (three choices required).
    /// </summary>
    public static Child CreateWithWishlist(string name, string behaviorKey, Toy firstChoice, Toy secondChoice, Toy thirdChoice)
    {
        var child = Create(name, behaviorKey);
        child.SetWishList(firstChoice, secondChoice, thirdChoice);
        return child;
    }

    /// <summary>
    /// Create a Child and set its wishlist (three choices required), using the provided resolver for factory resolution.
    /// </summary>
    public static Child CreateWithWishlist(string name, string behaviorKey, Toy firstChoice, Toy secondChoice, Toy thirdChoice, IBehaviorFactory behaviorFactory)
    {
        BehaviorFactoryProducer.GetFactoryOrThrow(behaviorKey ?? throw new ArgumentNullException(nameof(behaviorFactory)));
        return CreateWithWishlist(name, behaviorKey, firstChoice, secondChoice, thirdChoice);
    }
}
