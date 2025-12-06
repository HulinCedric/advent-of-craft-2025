namespace Gifts;

public static class BehaviorFactoryProducer
{
    /// <summary>
    /// Parse a behavior key (case-insensitive) and return a factory, or null if parsing fails.
    /// </summary>
    public static IBehaviorFactory? GetFactory(string? behaviorKey)
    {
        if (string.IsNullOrWhiteSpace(behaviorKey)) return null;
        return behaviorKey.Trim().ToLowerInvariant() switch
        {
            "naughty" => new NaughtyBehaviorFactory(),
            "nice" => new NiceBehaviorFactory(),
            "very nice" => new VeryNiceBehaviorFactory(),
            _ => null
        };
    }

    public static IBehaviorFactory GetFactoryOrThrow(string behaviorKey)
    {
        var factory = GetFactory(behaviorKey);
        return factory ?? throw new ArgumentException($"Unknown behavior: '{behaviorKey}'", nameof(behaviorKey));
    }
}
