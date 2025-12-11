using FluentAssertions;
using Xunit;

namespace AWorldWithoutMocksBefore.Tests;

public class ToyProductionServiceTests
{
    private const string ToyName = "Train";

    [Fact]
    public void AssignToyToElf_ShouldSaveToyInProduction_AndNotify()
    {
        var toyRepository = new FakeToyRepository();
        var notificationsService = new SpyNotificationService();
        var service = new ToyProductionService(toyRepository, notificationsService);
        var toy = new Toy(ToyName, ToyState.Unassigned);
        toyRepository.AlreadyContains(toy);

        service.AssignToyToElf(ToyName);

        var savedToy = toyRepository.FindByName(ToyName);
        savedToy.State.Should().Be(ToyState.InProduction);
        notificationsService.Notified().Should().ContainSingle().Which.Should().Be(savedToy);
    }

    [Fact]
    public void AssignToyToElf_ShouldNotSaveOrNotify_WhenToyNotFound()
    {
        var toyRepository = new FakeToyRepository();
        var notificationService = new SpyNotificationService();
        var service = new ToyProductionService(toyRepository, notificationService);
        toyRepository.WithoutToys();

        service.AssignToyToElf(ToyName);

        toyRepository.FindByName(ToyName).Should().BeNull();
        notificationService.Notified().Should().BeEmpty();
    }

    [Fact]
    public void AssignToyToElf_ShouldNotSaveOrNotify_WhenToyAlreadyInProduction()
    {
        var toyRepository = new FakeToyRepository();
        var notificationService = new SpyNotificationService();
        var service = new ToyProductionService(toyRepository, notificationService);
        var toy = new Toy(ToyName, ToyState.InProduction);
        toyRepository.AlreadyContains(toy);

        service.AssignToyToElf(ToyName);

        toyRepository.FindByName(ToyName).Should().Be(toy);
        notificationService.Notified().Should().BeEmpty();
    }
}

public class SpyNotificationService : INotificationService
{
    private readonly List<Toy> notifications = [];
    public void NotifyToyAssigned(Toy toy) => notifications.Add(toy);

    public IReadOnlyList<Toy> Notified() => notifications;
}

public class FakeToyRepository : IToyRepository
{
    private readonly Dictionary<string, Toy> _toys = [];
    public Toy? FindByName(string name) => _toys.GetValueOrDefault(name);

    public void Save(Toy toy) => _toys[toy.Name] = toy;

    public void AlreadyContains(Toy toy) => _toys[toy.Name] = toy;

    public void WithoutToys() => _toys.Clear();
}