global using Xunit;
using FluentAssertions;

namespace Gifts.Tests;

public class SantaTest
{
    private static readonly Toy Playstation = new("playstation");
    private static readonly Toy Ball = new("ball");
    private static readonly Toy Plush = new("plush");
    private readonly ChildFactory _childFactory;
    private readonly Santa _santa;

    public SantaTest()
    {
        IBehaviorFactoryResolver behaviorFactoryResolver = new BehaviorFactoryResolver();
        behaviorFactoryResolver.Register("naughty", new NaughtyBehaviorFactory());
        behaviorFactoryResolver.Register("nice", new NiceBehaviorFactory());
        behaviorFactoryResolver.Register("very nice", new VeryNiceBehaviorFactory());
       
        _childFactory = new ChildFactory(behaviorFactoryResolver);
        
        IChildRepository repo = new InMemoryChildRepository();
        
        _santa = new Santa(repo);
    }

    [Fact]
    public void GivenNaughtyChildWhenDistributingGiftsThenChildReceivesThirdChoice()
    {
        var bobby = _childFactory.Create("bobby", "naughty");
        bobby.SetWishList(Playstation, Plush, Ball);

        _santa.AddChild(bobby);
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(Ball);
    }

    [Fact]
    public void GivenNiceChildWhenDistributingGiftsThenChildReceivesSecondChoice()
    {
        var bobby = _childFactory.Create("bobby", "nice");
        bobby.SetWishList(Playstation, Plush, Ball);

        _santa.AddChild(bobby);
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(Plush);
    }

    [Fact]
    public void GivenVeryNiceChildWhenDistributingGiftsThenChildReceivesFirstChoice()
    {
        var bobby = _childFactory.Create("bobby", "very nice");
        bobby.SetWishList(Playstation, Plush, Ball);

        _santa.AddChild(bobby);
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(Playstation);
    }

    [Fact]
    public void GivenNonExistingChildWhenDistributingGiftsThenExceptionThrown()
    {
        var bobby = _childFactory.Create("bobby", "very nice");
        bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);

        var chooseToyForChild = () => _santa.ChooseToyForChild("alice");
        chooseToyForChild.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("No such child found");
    }
}