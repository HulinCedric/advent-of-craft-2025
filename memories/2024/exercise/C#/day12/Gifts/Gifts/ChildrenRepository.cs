namespace Gifts;

public class ChildrenRepository : IChildrenRepository
{
    private readonly List<Child> _children = [];
    
    public Child? GetChild(string childName) 
        => _children.FirstOrDefault(currentChild => currentChild.Name == childName); 

    public void Add(Child child) 
        => _children.Add(child);
}