using System.Collections;
using FluentAssertions;

namespace SantaGiftDispatcher.Tests;

public class SantaGiftDispatcherTests
{
    [Fact]
    public void Behavior_of_AI_refactored_app()
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

        results.Should()
            .BeEquivalentTo(
                [
                    new SantaGiftDispatcher.GiftAssignment("Alice", "Doll"),
                    new SantaGiftDispatcher.GiftAssignment("Alice", "Doll"),
                    new SantaGiftDispatcher.GiftAssignment("Bob", "Train")
                ],
                options => options.WithStrictOrdering());
    }
}