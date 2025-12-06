global using Xunit;
using FluentAssertions.LanguageExt;
using LanguageExt;

namespace Gifts.Tests;

public class SantaTest
{
    private readonly Santa _santa = new(new InMemoryChildrenRepository());

    private static readonly Toy Playstation = new("playstation");
    private static readonly Toy Ball = new("ball");
    private static readonly Toy Plush = new("plush");
    private static readonly Option<Toy> NoToy = Option<Toy>.None;

    [Fact]
    public void GivenNaughtyChildWhenDistributingGiftsThenChildReceivesThirdChoice()
    {
        var bobby = new Child("bobby", Behavior.Naughty);
        bobby = bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);
      
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(Ball);
    }

    [Fact]
    public void GivenNiceChildWhenDistributingGiftsThenChildReceivesSecondChoice()
    {
        var bobby = new Child("bobby", Behavior.Nice);
        bobby = bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);
       
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(Plush);
    }

    [Fact]
    public void GivenVeryNiceChildWhenDistributingGiftsThenChildReceivesFirstChoice()
    {
        var bobby = new Child("bobby", Behavior.VeryNice);
        bobby = bobby.SetWishList(Playstation, Plush, Ball);
        _santa.AddChild(bobby);
       
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(Playstation);
    }

    [Fact]
    public void GivenNonExistingChildWhenDistributingGiftsThenFailed()
    {
        var bobby = new Child("bobby", Behavior.VeryNice);
        bobby = bobby.SetWishList(Playstation, Plush, Ball);
       
        _santa.AddChild(bobby);

        var got = _santa.ChooseToyForChild("alice");
        got.Should().Be("No such child found");
    }
    
    [Fact]
    public void GivenChildWithoutWishListWhenDistributingGiftsThenChildReceivesNoToy()
    {
        var bobby = new Child("bobby", Behavior.VeryNice);
        _santa.AddChild(bobby);
       
        var got = _santa.ChooseToyForChild("bobby");

        got.Should().Be(NoToy);
    }
}