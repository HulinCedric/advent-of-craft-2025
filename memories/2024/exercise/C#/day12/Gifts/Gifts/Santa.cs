namespace Gifts;

public class Santa
{
    private readonly IChildRepository _childrenRepository;

    public Santa(IChildRepository childrenRepository) => _childrenRepository = childrenRepository;

    public Toy? ChooseToyForChild(string childName)
    {
        var found = _childrenRepository.GetByName(childName);

        if (found == null)
            throw new InvalidOperationException("No such child found");

        return found.ChooseToy();
    }

    public void AddChild(Child child) => _childrenRepository.Add(child);
}