using LanguageExt;

namespace ControlSystem.Core;

public class HarnessedReindeers
{
    private const int RequiredMagicPowerForAscend = 40;

    private readonly IReadOnlyList<ReindeerPowerUnit> _reindeers;

    internal HarnessedReindeers(IEnumerable<ReindeerPowerUnit> reindeers) => _reindeers = reindeers.ToList();

    public Either<string, Unit> HarnessAllPower()
    {
        if (!HasEnoughPowerToReach(RequiredMagicPowerForAscend))
            return "The reindeer needs rest. Please park the sleigh...";

        foreach (var reindeer in _reindeers)
        {
            reindeer.HarnessMagicPower();
        }

        return Unit.Default;
    }

    private bool HasEnoughPowerToReach(int powerNeeded) => _reindeers.Sum(r => r.CheckMagicPower()) >= powerNeeded;

    public void RestReindeers()
    {
        foreach (var reindeer in _reindeers)
        {
            reindeer.ReleaseHarness();
        }
    }
}