using System.Collections.Concurrent;

namespace Gifts;

public sealed class BehaviorFactoryResolver : IBehaviorFactoryResolver
{
    private readonly ConcurrentDictionary<string, IBehaviorFactory> _map = new(StringComparer.OrdinalIgnoreCase);

    public BehaviorFactoryResolver()
    {
        // register default factories
        Register("naughty", new NaughtyBehaviorFactory());
        Register("nice", new NiceBehaviorFactory());
        Register("very nice", new VeryNiceBehaviorFactory());
    }

    public void Register(string behaviorKey, IBehaviorFactory factory)
    {
        if (string.IsNullOrWhiteSpace(behaviorKey)) throw new ArgumentNullException(nameof(behaviorKey));
        if (factory == null) throw new ArgumentNullException(nameof(factory));
        _map[behaviorKey.Trim()] = factory;
    }

    public IBehaviorFactory? GetFactory(string? behaviorKey)
    {
        if (string.IsNullOrWhiteSpace(behaviorKey)) return null;
        return _map.TryGetValue(behaviorKey.Trim(), out var factory) ? factory : null;
    }
}

