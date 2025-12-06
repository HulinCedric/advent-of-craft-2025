global using Xunit;
using FluentAssertions;

namespace Gifts.Tests;

public class SantaTest
{
    private static readonly Toy Playstation = new("playstation");
    private static readonly Toy Ball = new("ball");
    private static readonly Toy Plush = new("plush");

    [Fact]
    public void GivenNaughtyChildWhenDistributingGiftsThenChildReceivesThirdChoice()
    {
        var repo = new InMemoryChildRepository();
        var bobby = ChildFactory.Create("bobby", "naughty");
        bobby.SetWishList(Playstation, Plush, Ball);
        var santa = new Santa(repo);
        santa.AddChild(bobby);
        var got = santa.ChooseToyForChild("bobby");

        got.Should().Be(Ball);
    }

    [Fact]
    public void GivenNiceChildWhenDistributingGiftsThenChildReceivesSecondChoice()
    {
        var repo = new InMemoryChildRepository();
        var bobby = ChildFactory.Create("bobby", "nice");
        bobby.SetWishList(Playstation, Plush, Ball);
        var santa = new Santa(repo);
        santa.AddChild(bobby);
        var got = santa.ChooseToyForChild("bobby");

        got.Should().Be(Plush);
    }

    [Fact]
    public void GivenVeryNiceChildWhenDistributingGiftsThenChildReceivesFirstChoice()
    {
        var repo = new InMemoryChildRepository();
        var bobby = ChildFactory.Create("bobby", "very nice");
        bobby.SetWishList(Playstation, Plush, Ball);
        var santa = new Santa(repo);
        santa.AddChild(bobby);
        var got = santa.ChooseToyForChild("bobby");

        got.Should().Be(Playstation);
    }

    [Fact]
    public void GivenNonExistingChildWhenDistributingGiftsThenExceptionThrown()
    {
        var repo = new InMemoryChildRepository();
        var santa = new Santa(repo);
        var bobby = ChildFactory.Create("bobby", "very nice");
        bobby.SetWishList(Playstation, Plush, Ball);
        santa.AddChild(bobby);

        var chooseToyForChild = () => santa.ChooseToyForChild("alice");
        chooseToyForChild.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("No such child found");
    }
}