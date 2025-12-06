namespace Gifts;

public class Santa
{
    private readonly List<Child> _childrenRepository = new();

    public Toy? ChooseToyForChild(string childName)
    {
        Child? found = null;
        for (int i = 0; i < _childrenRepository.Count; i++)
        {
            var currentChild = _childrenRepository[i];
            if (currentChild.Name == childName)
            {
                found = currentChild;
            }
        }

        if (found == null)
            throw new InvalidOperationException("No such child found");

        return found.Behavior.ChooseToy(found.Wishlist);
    }

    public void AddChild(Child child) => _childrenRepository.Add(child);
}