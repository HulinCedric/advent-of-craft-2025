namespace AWorldWithoutMocksBefore.Tests.TestDoubles;

public class SpyNotificationService : INotificationService
{
    private readonly List<Toy> _notifications = [];

    public void NotifyToyAssigned(Toy toy) => _notifications.Add(toy);

    public IReadOnlyList<Toy> Notified() => _notifications;
}