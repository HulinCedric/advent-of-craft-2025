namespace Gifts;

public interface IChildrenRepository
{
    Child? GetChild(string childName);
    void Add(Child child);
}