namespace Gifts;

public static class ChildFactory
{
    /// <summary>
    ///     Create a Child with the given name and behavior key (string). The behavior key is validated via
    ///     BehaviorFactoryProducer.
    /// </summary>
    public static Child Create(string name, string behaviorKey)
    {
        var factory = BehaviorFactoryProducer.GetFactory(behaviorKey);
        if (factory == null)
            throw new ArgumentException($"Unknown behavior key: '{behaviorKey}'", nameof(behaviorKey));

        var behavior = factory.Create();

        return new Child(name, behavior);
    }
}