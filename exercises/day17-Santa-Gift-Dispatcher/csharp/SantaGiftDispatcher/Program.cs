namespace SantaGiftDispatcher;

internal static class Program
{
    private static void Main()
    {
        var inventory = new Dictionary<string, int>
        {
            ["Train"] = 1,
            ["Doll"] = 2,
            ["Coal"] = 0
        };

        var dispatcher = new SantaGiftDispatcher(inventory);

        dispatcher.RegisterChild("Alice", ["Doll", "Train"]);
        dispatcher.RegisterChild("Bob", ["Train", "Doll"]);
        dispatcher.RegisterChild("Charlie", ["Puzzle"]);

        var results = dispatcher.Dispatch(maxGiftsPerChild: 2);

        foreach (var assignment in results)
        {
            Console.WriteLine(assignment);
        }
    }
}