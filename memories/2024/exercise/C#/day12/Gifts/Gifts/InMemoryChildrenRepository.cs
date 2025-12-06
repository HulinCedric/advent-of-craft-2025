using LanguageExt;

namespace Gifts;

public sealed class InMemoryChildrenRepository : IChildrenRepository
{
    private Seq<Child> _children = Seq<Child>.Empty;

    public Option<Child> FindChildByName(ChildName childName) => _children.Find(child => child.IsNamed(childName));

    public void AddChild(Child child) => _children = _children.Add(child);
}