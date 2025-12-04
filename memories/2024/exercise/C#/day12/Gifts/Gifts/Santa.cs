namespace Gifts;

public class Santa(IChildrenRepository repository)
{
    public Toy? ChooseToyForChild(string childName)
    {
        return repository.GetChild(childName) == null 
            ? throw new InvalidOperationException("No such child found") 
            : repository.GetChild(childName)?.GetDeservedToy();
    }
}