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
        var repoMock = new Mock<IToyRepository>();
        var notificationMock = new Mock<INotificationService>();
        var service = new ToyProductionService(repoMock.Object, notificationMock.Object);
        repoMock.Setup(r => r.FindByName(ToyName)).Returns((Toy?)null);

        service.AssignToyToElf(ToyName);

        repoMock.Verify(r => r.FindByName(ToyName), Times.Once);
        repoMock.Verify(r => r.Save(It.IsAny<Toy>()), Times.Never);
        notificationMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void AssignToyToElf_ShouldNotSaveOrNotify_WhenToyAlreadyInProduction()
    {
        var repoMock = new Mock<IToyRepository>();
        var notificationMock = new Mock<INotificationService>();
        var service = new ToyProductionService(repoMock.Object, notificationMock.Object);
        var toy = new Toy(ToyName, ToyState.InProduction);
        repoMock.Setup(r => r.FindByName(ToyName)).Returns(toy);

        service.AssignToyToElf(ToyName);

        repoMock.Verify(r => r.FindByName(ToyName), Times.Once);
        repoMock.Verify(r => r.Save(It.IsAny<Toy>()), Times.Never);
        notificationMock.VerifyNoOtherCalls();
    }
}

public class FakeToyRepository : IToyRepository
{
    private readonly List<Toy> _toys = [];
    public Toy? FindByName(string name) => _toys.FirstOrDefault(t => t.Name == name);

    public void Save(Toy toy)
    {
        _toys.RemoveAll(t => t.Name == toy.Name);
        _toys.Add(toy);
    }

    public void AlreadyContains(Toy toy) => _toys.Add(toy);
}