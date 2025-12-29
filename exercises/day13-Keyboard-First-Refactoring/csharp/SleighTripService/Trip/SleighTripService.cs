using SleighTripService.Elf;
using SleighTripService.Exception;

namespace SleighTripService.Trip;

public class SleighTripService
{
    public List<SleightTrip> GetTripsByUser(Elf.Elf targetElf)
    {
        var sleighTrips = new List<SleightTrip>();
        var loggedElf = GetLoggedUser();
        var isWorkshopFriend = false;

        if (loggedElf != null)
        {
            foreach (var friend in targetElf.GetFriends())
            {
                if (friend.Equals(loggedElf))
                {
                    isWorkshopFriend = true;
                    break;
                }
            }
            if (isWorkshopFriend)
            {
                sleighTrips = FindTripsByUser(targetElf);
            }
            return sleighTrips;
        }
        else
        {
            throw new ElfNotLoggedInException();
        }
    }

    protected virtual List<SleightTrip> FindTripsByUser(Elf.Elf targetElf)
    {
        return SleighTripDAO.FindTripsByUser(targetElf);
    }

    protected virtual Elf.Elf? GetLoggedUser()
    {
        return ElfSession.GetInstance().GetLoggedUser();
    }
}
