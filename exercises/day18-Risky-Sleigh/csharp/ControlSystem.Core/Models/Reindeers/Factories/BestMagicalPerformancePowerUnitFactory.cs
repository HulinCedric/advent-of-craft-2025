using ControlSystem.Core.Models.Reindeers.Ports;

namespace ControlSystem.Core.Models.Reindeers.Factories;

public class BestMagicalPerformancePowerUnitFactory(
    IReadOnlyList<IReindeer> allReindeers,
    IReadOnlyDictionary<int, AmplifierType> availableAmplifierByMagicalPower)
    : IPowerUnitFactory
{
    public HarnessedReindeers BringAllReindeers()
    {
        var allReindeerByMagicalPower = allReindeers
            .OrderByDescending(r => r.GetMagicPower())
            .ToList();

        return new HarnessedReindeers(
            allReindeerByMagicalPower
                .Select((reindeer, index) => AttachPowerUnit(reindeer, index + 1)));
    }

    private ReindeerPowerUnit AttachPowerUnit(IReindeer reindeer, int indexOfMagicalPower)
        => GeneratePowerUnit(
            reindeer,
            availableAmplifierByMagicalPower.GetValueOrDefault(indexOfMagicalPower, AmplifierType.Basic));

    private static ReindeerPowerUnit GeneratePowerUnit(IReindeer reindeer, AmplifierType amplifierToAttach)
        => new(reindeer, new MagicPowerAmplifier(amplifierToAttach));
}