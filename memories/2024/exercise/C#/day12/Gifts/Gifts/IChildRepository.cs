namespace Gifts;

public interface IChildRepository
{
    void Add(Child child);
    Child? GetByName(string name);
    IReadOnlyCollection<Child> GetAll();
}

