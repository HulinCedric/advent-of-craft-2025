using FluentAssertions;
using Moq;
using Xunit;

namespace AWorldWithoutMocksBefore.Tests;

public class ToyProductionServiceTests
{
    private const string ToyName = "Train";

    [Fact]
    public void AssignToyToElf_ShouldSaveToyInProduction_AndNotify()
    {
        var toyRepository = new FakeToyRepository();
        var notificationMock = new Mock<INotificationService>();
        var service = new ToyProductionService(toyRepository, notificationMock.Object);
        var toy = new Toy(ToyName, ToyState.Unassigned);
        toyRepository.AlreadyContains(toy);

        service.AssignToyToElf(ToyName);

        toyRepository.FindByName(ToyName)!.State.Should().Be(ToyState.InProduction);
        notificationMock.Verify(
            n => n.NotifyToyAssigned(It.Is<Toy>(t => t.State == ToyState.InProduction)),
            Times.Once);
    }

    [Fact]
    public void AssignToyToElf_ShouldNotSaveOrNotify_WhenToyNotFound()
    {
        var toyRepository = new FakeToyRepository();
        var notificationMock = new Mock<INotificationService>();
        var service = new ToyProductionService(toyRepository, notificationMock.Object);
        toyRepository.WithoutToys();

        service.AssignToyToElf(ToyName);
        
        toyRepository.FindByName(ToyName).Should().BeNull();
        notificationMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void AssignToyToElf_ShouldNotSaveOrNotify_WhenToyAlreadyInProduction()
    {
        var toyRepository = new FakeToyRepository();
        var notificationMock = new Mock<INotificationService>();
        var service = new ToyProductionService(toyRepository, notificationMock.Object);
        var toy = new Toy(ToyName, ToyState.InProduction);
        toyRepository.AlreadyContains(toy);
        
        service.AssignToyToElf(ToyName);

        toyRepository.FindByName(ToyName).Should().Be(toy);
        notificationMock.VerifyNoOtherCalls();
    }
}

public class FakeToyRepository : IToyRepository
{
    private readonly Dictionary<string, Toy> _toys = [];
    public Toy? FindByName(string name) => _toys.GetValueOrDefault(name);

    public void Save(Toy toy) => _toys[toy.Name] = toy;

    public void AlreadyContains(Toy toy) => _toys[toy.Name] = toy;

    public void WithoutToys() => _toys.Clear();
}