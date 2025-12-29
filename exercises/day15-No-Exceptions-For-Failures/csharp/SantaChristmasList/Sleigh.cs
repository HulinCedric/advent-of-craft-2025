namespace SantaChristmasList;

public class Sleigh
{
    private readonly Dictionary<Child, string> _messages = new();

    public void Put(Child child, string message) => _messages[child] = message;

    public IReadOnlyDictionary<Child, string> Messages => _messages;
}