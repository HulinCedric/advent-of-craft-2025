using FluentAssertions;
using SleighTripService.Exception;
using SleighTripService.Trip;
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
    public void ReturnNoTripsWhenLoggedInElfHaveNoFriends()
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
    public void ReturnNoTripsWhenLoggedInElfIsNotFriendOfTargetElf()
    {
        // given
        var loggedUser = new Elf.Elf();
        var tripService = new TestableSleighTripService(loggedUser);
        var targetElf = new Elf.Elf();

        var anotherFriend = new Elf.Elf();
        targetElf.AddFriend(anotherFriend);

        // when
        var trips = tripService.GetTripsByUser(targetElf);

        // then
        trips.Should().BeEmpty();
    }

    [Fact]
    public void ReturnTargetElfTripsWhenLoggedInElfAskForWorkshopFriendTrips()
    {
        // given
        var loggedUser = new Elf.Elf();
        var tripService = new TestableSleighTripService(loggedUser);
        var targetElf = new Elf.Elf();
        
        targetElf.AddFriend(loggedUser);

        var sleightTrip = new SleightTrip();
        tripService.Containing(sleightTrip);

        // when
        tripService.GetTripsByUser(targetElf).Should().BeEquivalentTo([sleightTrip]);
    }

    private class TestableSleighTripService : Trip.SleighTripService
    {
        private readonly Elf.Elf? _loggedUser;
        private readonly List<SleightTrip> _trips = [];

        public TestableSleighTripService(Elf.Elf? loggedUser) => _loggedUser = loggedUser;

        protected override Elf.Elf? GetLoggedUser() => _loggedUser;

        protected override List<SleightTrip> FindTripsByUser(Elf.Elf targetElf) => _trips;

        public void Containing(SleightTrip sleightTrip) => _trips.Add(sleightTrip);
    }
}