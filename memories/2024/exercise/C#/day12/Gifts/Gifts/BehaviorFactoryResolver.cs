using System.Collections.Concurrent;

namespace Gifts;

public sealed class BehaviorFactoryResolver : IBehaviorFactoryResolver
{
    private readonly ConcurrentDictionary<string, IBehaviorFactory> _map = new(StringComparer.OrdinalIgnoreCase);

    public void Register(string behaviorKey, IBehaviorFactory factory)
    {
        if (string.IsNullOrWhiteSpace(behaviorKey)) throw new ArgumentNullException(nameof(behaviorKey));

        _map[behaviorKey.Trim()] = factory;
    }

    public IBehaviorFactory? GetFactory(string behaviorKey) => _map.GetValueOrDefault(behaviorKey.Trim());
}