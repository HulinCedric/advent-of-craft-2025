using AWorldWithoutMocksBefore.Tests.TestDoubles;
using FluentAssertions;

namespace AWorldWithoutMocksBefore.Tests.Verifications;

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