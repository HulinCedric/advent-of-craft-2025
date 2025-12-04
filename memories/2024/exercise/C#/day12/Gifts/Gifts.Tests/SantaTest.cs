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
        var bobby = new Child("bobby", BehaviorType.NAUGHTY);
        bobby.SetWishList(Playstation, Plush, Ball);

        var childrenRepository = new ChildrenRepository();
        childrenRepository.Add(bobby);
        
        var santa = new Santa(childrenRepository);
        var got = santa.ChooseToyForChild("bobby");

        got.Should().Be(Ball);
    }

    [Fact]
    public void GivenNiceChildWhenDistributingGiftsThenChildReceivesSecondChoice()
    {
        var bobby = new Child("bobby", BehaviorType.NICE);
        bobby.SetWishList(Playstation, Plush, Ball);
        var childrenRepository = new ChildrenRepository();
        childrenRepository.Add(bobby);
        
        var santa = new Santa(childrenRepository);
        var got = santa.ChooseToyForChild("bobby");

        got.Should().Be(Plush);
    }

    [Fact]
    public void GivenVeryNiceChildWhenDistributingGiftsThenChildReceivesFirstChoice()
    {
        var bobby = new Child("bobby", BehaviorType.VERY_NICE);
        bobby.SetWishList(Playstation, Plush, Ball);
        var childrenRepository = new ChildrenRepository();
        childrenRepository.Add(bobby);
        
        var santa = new Santa(childrenRepository);
        var got = santa.ChooseToyForChild("bobby");

        got.Should().Be(Playstation);
    }

    [Fact]
    public void GivenNonExistingChildWhenDistributingGiftsThenExceptionThrown()
    {
        var bobby = new Child("bobby", BehaviorType.VERY_NICE);
        bobby.SetWishList(Playstation, Plush, Ball);

        var childrenRepository = new ChildrenRepository();
        childrenRepository.Add(bobby);

        var santa = new Santa(childrenRepository);

        var chooseToyForChild = () => santa.ChooseToyForChild("alice");
        chooseToyForChild.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("No such child found");
    }
}