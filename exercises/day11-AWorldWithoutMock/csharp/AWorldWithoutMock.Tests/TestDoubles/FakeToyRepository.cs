namespace AWorldWithoutMocksBefore.Tests.TestDoubles;

public class FakeToyRepository : IToyRepository
{
    private readonly Dictionary<string, Toy> _toys = [];
    public Toy? FindByName(string name) => _toys.GetValueOrDefault(name);

    public void Save(Toy toy) => _toys[toy.Name] = toy;

    public void AlreadyContains(Toy toy) => _toys[toy.Name] = toy;

    public void WithoutToys() => _toys.Clear();
}