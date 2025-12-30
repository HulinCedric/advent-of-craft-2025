namespace SantaGiftDispatcher;

using System;
using System.Collections;

internal static class Program
{
    private static void Main()
    {
        var inventory = new Hashtable
        {
            ["Train"] = 1,
            ["Doll"] = 2,
            ["Coal"] = 0
        };

        var dispatcher = new SantaGiftDispatcher(inventory);

        dispatcher.RegisterChild("Alice", new ArrayList { "Doll", "Train" });
        dispatcher.RegisterChild("Bob", new ArrayList { "Train", "Doll" });
        dispatcher.RegisterChild("Charlie", new ArrayList { "Puzzle" });

        var results = dispatcher.Dispatch(maxGiftsPerChild: 2);

        foreach (var assignment in results)
        {
            Console.WriteLine(assignment);
        }
    }
}
