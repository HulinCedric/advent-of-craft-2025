using FluentAssertions;
using SleighTripService.Exception;
using Xunit;

namespace SleighTripService.Tests;

public class SleighTripServiceShould
{
    [Fact]
    public void ThrowExceptionWhenNoElfIsLoggedIn()
    {
        // given
        var tripService = new TestableSleighTripService(null);
        var targetElf = new Elf.Elf();

        // when / then
        var act = () => tripService.GetTripsByUser(targetElf);
        act.Should().Throw<ElfNotLoggedInException>();
    }

    [Fact]
    public void ReturnNoTripsWhenLoggedInElfIsNotFriendOfTargetElf()
    {
        // given
        var tripService = new TestableSleighTripService(new Elf.Elf());
        var targetElf = new Elf.Elf();

        // when
        var trips = tripService.GetTripsByUser(targetElf);

        // then
        trips.Should().BeEmpty();
    }

    [Fact]
    public void ThrowExceptionWhenTripsWithWorkshopFriend()
    {
        // given
        var loggedUser = new Elf.Elf();
        var tripService = new TestableSleighTripService(loggedUser);
        var targetElf = new Elf.Elf();

        targetElf.AddFriend(loggedUser);

        // when
        var act = () => tripService.GetTripsByUser(targetElf);
        act.Should()
            .Throw<CollaboratorCallException>()
            .Which.Message.Should()
            .Be("SleighTripDAO is a static collaborator and should not be used directly in unit tests");
    }

    private class TestableSleighTripService : Trip.SleighTripService
    {
        private readonly Elf.Elf? _loggedUser;

        public TestableSleighTripService(Elf.Elf? loggedUser) => _loggedUser = loggedUser;

        protected override Elf.Elf? GetLoggedUser() => _loggedUser;
    }
}