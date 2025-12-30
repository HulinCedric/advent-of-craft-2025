using LanguageExt.Common;
using GiftProcessResult = (SantaChristmasList.Child child, SantaChristmasList.Gift gift);

namespace SantaChristmasList;

public record LoadGiftsInSleighResult(IReadOnlyList<Error> Failures, Sleigh Sleigh)
{
    public static LoadGiftsInSleighResult Create((IEnumerable<Error>, IEnumerable<GiftProcessResult>) partition)
    {
        var (failures, successes) = partition;
        var sleigh = successes.Fold(new Sleigh(), LoadGiftInSleigh);
        return new LoadGiftsInSleighResult(failures.ToList(), sleigh);
    }

    private static Sleigh LoadGiftInSleigh(Sleigh sleigh, GiftProcessResult result)
    {
        sleigh.Put(result.child, $"Gift: {result.gift.Name} has been loaded!");
        return sleigh;
    }
}