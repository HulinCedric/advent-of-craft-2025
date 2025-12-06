using System.Collections.Concurrent;

namespace Gifts;

public sealed class InMemoryChildRepository : IChildRepository
{
    private readonly ConcurrentDictionary<string, Child> _children = new(StringComparer.OrdinalIgnoreCase);

    public void Add(Child child) => _children[child.Name] = child;

    public Child? GetByName(string name)
        => string.IsNullOrWhiteSpace(name)
            ? null
            : _children.GetValueOrDefault(name);
}