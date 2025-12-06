using System.Collections.Concurrent;

namespace Gifts;

public sealed class InMemoryChildRepository : IChildRepository
{
    private readonly ConcurrentDictionary<string, Child> _children = new(StringComparer.OrdinalIgnoreCase);

    public void Add(Child child)
    {
        if (child == null) throw new ArgumentNullException(nameof(child));
        _children[child.Name] = child;
    }

    public Child? GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        return _children.TryGetValue(name, out var child) ? child : null;
    }

    public IReadOnlyCollection<Child> GetAll() => _children.Values.ToList().AsReadOnly();
}

