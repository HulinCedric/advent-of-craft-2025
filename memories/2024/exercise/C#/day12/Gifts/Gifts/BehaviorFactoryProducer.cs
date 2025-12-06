namespace Gifts;

public static class BehaviorFactoryProducer
{
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
}