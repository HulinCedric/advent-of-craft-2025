using LanguageExt;

namespace Gifts;

public sealed class InMemoryChildrenRepository : IChildrenRepository
{
    private Map<ChildName, Child> _children = Map<ChildName, Child>.Empty;

    public Option<Child> FindChildByName(ChildName childName) => _children.Find(childName);

    public void AddChild(Child child) => _children = _children.Add(child.Name, child);
}