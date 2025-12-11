using FluentAssertions;
using FluentAssertions.Primitives;
using Xunit;

namespace AWorldWithoutMocksBefore.Tests;

public class ToyProductionServiceTests
{
    private const string ToyName = "Train";

    private readonly SpyNotificationService _notificationService;
    private readonly ToyProductionService _service;
    private readonly FakeToyRepository _toyRepository;

    public ToyProductionServiceTests()
    {
        _toyRepository = new FakeToyRepository();
        _notificationService = new SpyNotificationService();
        _service = new ToyProductionService(_toyRepository, _notificationService);
    }

    [Fact]
    public void AssignToyToElf_ShouldSaveToyInProduction_AndNotify()
    {
        _toyRepository.AlreadyContains(new Toy(ToyName, ToyState.Unassigned));

        _service.AssignToyToElf(ToyName);

        _toyRepository.ShouldHaveSavedToy(ToyName).InProduction();
        _notificationService.ShouldHaveNotifiedToyAssigned(ToyName).InProduction();
    }

    [Fact]
    public void AssignToyToElf_ShouldNotSaveOrNotify_WhenToyNotFound()
    {
        _toyRepository.WithoutToys();

        _service.AssignToyToElf(ToyName);

        _toyRepository.ShouldNotHaveSavedToy(ToyName);
        _notificationService.ShouldNotHaveNotifiedAnyToyAssigned();
    }

    [Fact]
    public void AssignToyToElf_ShouldNotSaveOrNotify_WhenToyAlreadyInProduction()
    {
        var toy = new Toy(ToyName, ToyState.InProduction);
        _toyRepository.AlreadyContains(toy);

        _service.AssignToyToElf(ToyName);

        _toyRepository.ShouldHaveSavedToy(ToyName).InProduction();
        _notificationService.ShouldNotHaveNotifiedAnyToyAssigned();
    }
}

public class SpyNotificationService : INotificationService
{
    private readonly List<Toy> _notifications = [];

    public void NotifyToyAssigned(Toy toy) => _notifications.Add(toy);

    public IReadOnlyList<Toy> Notified() => _notifications;
}

public class FakeToyRepository : IToyRepository
{
    private readonly Dictionary<string, Toy> _toys = [];
    public Toy? FindByName(string name) => _toys.GetValueOrDefault(name);

    public void Save(Toy toy) => _toys[toy.Name] = toy;

    public void AlreadyContains(Toy toy) => _toys[toy.Name] = toy;

    public void WithoutToys() => _toys.Clear();
}

public static class ToyRepositoryVerificationExtensions
{
    public static Toy ShouldHaveSavedToy(this FakeToyRepository toyRepository, string toyName)
        => toyRepository.ToyWithName(toyName);

    private static Toy ToyWithName(this FakeToyRepository toyRepository, string toyName)
    {
        var toy = toyRepository.FindByName(toyName);
        toy.Should().NotBeNull();
        return toy;
    }

    public static void ShouldNotHaveSavedToy(this FakeToyRepository toyRepository, string toyName)
        => toyRepository.FindByName(toyName).Should().BeNull();
}

public static class NotificationServiceVerificationExtensions
{
    public static Toy ShouldHaveNotifiedToyAssigned(this SpyNotificationService notificationService, string toyName)
        => notificationService.Notified()
            .Should()
            .ContainSingle(toy => toy.Name == toyName)
            .Which;

    public static void ShouldNotHaveNotifiedAnyToyAssigned(this SpyNotificationService notificationService)
        => notificationService.Notified()
            .Should()
            .BeEmpty();
}

public static class ToyVerificationExtensions
{
    public static AndConstraint<EnumAssertions<ToyState>> InProduction(this Toy toy)
        => toy
            .State.Should()
            .Be(ToyState.InProduction);
}