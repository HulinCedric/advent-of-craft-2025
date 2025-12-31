using System.Collections;
using FluentAssertions;

namespace SantaGiftDispatcher.Tests;

public class SantaGiftDispatcherTests
{
    [Fact]
    public void Behavior_of_AI_refactored_app()
    {
        var inventory = new Dictionary<string, int>
        {
            ["Train"] = 2,
            ["Doll"] = 1,
            ["Coal"] = 1
        };

        var dispatcher = new SantaGiftDispatcher(inventory);

        dispatcher.RegisterChild("Alice", ["Doll", "Train"]);
        dispatcher.RegisterChild("Bob", ["Train", "Doll"]);
        dispatcher.RegisterChild("Charlie", ["Puzzle"]);

        var results = dispatcher.Dispatch(maxGiftsPerChild: 2);

        results.Should()
            .BeEquivalentTo(
                [
                    new SantaGiftDispatcher.GiftAssignment("Alice", "Doll"),
                    new SantaGiftDispatcher.GiftAssignment("Alice", "Train"),
                    new SantaGiftDispatcher.GiftAssignment("Bob", "Train"),
                    new SantaGiftDispatcher.GiftAssignment("Bob", "Coal"),
                ],
                options => options.WithStrictOrdering());
    }

    [Fact]
    public void Behavior_of_original_refactored_app()
    {
        var inventory = new Hashtable
        {
            ["Train"] = 2,
            ["Doll"] = 1,
            ["Coal"] = 1
        };

        var dispatcher = new D(inventory);

        dispatcher.A("Alice", new ArrayList { "Doll", "Train" });
        dispatcher.A("Bob", new ArrayList { "Train", "Doll" });
        dispatcher.A("Charlie", new ArrayList { "Puzzle" });

        var results = dispatcher.B(z: 2);

        results.Should()
            .BeEquivalentTo(
                new ArrayList
                {
                    new D.G("Alice", "Doll"),
                    new D.G("Alice", "Train"),
                    new D.G("Bob", "Train"),
                    new D.G("Bob", "Coal")
                },
                options => options.WithStrictOrdering());
    }
}