using LanguageExt;

namespace Gifts;

public interface IChildrenRepository
{
    Option<Child> FindChildByName(ChildName childName);
    void AddChild(Child child);
}