using AWorldWithoutMocksBefore.Tests.TestsDoubles;
using FluentAssertions;

namespace AWorldWithoutMocksBefore.Tests.Verifications;

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