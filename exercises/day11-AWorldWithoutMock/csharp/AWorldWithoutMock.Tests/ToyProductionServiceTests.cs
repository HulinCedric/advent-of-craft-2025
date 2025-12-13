using AWorldWithoutMocksBefore.Tests.TestDoubles;
using AWorldWithoutMocksBefore.Tests.Verifications;
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
        _toyRepository.AlreadyContains(new Toy(ToyName, ToyState.InProduction));

        _service.AssignToyToElf(ToyName);

        _toyRepository.ShouldHaveSavedToy(ToyName).InProduction();
        _notificationService.ShouldNotHaveNotifiedAnyToyAssigned();
    }
}