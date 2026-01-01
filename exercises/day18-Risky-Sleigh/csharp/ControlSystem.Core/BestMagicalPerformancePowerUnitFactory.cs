namespace ControlSystem.Core;

public class BestMagicalPerformancePowerUnitFactory(
    IReadOnlyList<IReindeer> allReindeers,
    IReadOnlyDictionary<int, AmplifierType> availableAmplifierByMagicalPower)
    : IPowerUnitFactory
{
    public List<ReindeerPowerUnit> BringAllReindeers()
    {
        var allReindeerByMagicalPower = allReindeers
            .OrderByDescending(r => r.GetMagicPower())
            .ToList();

        return allReindeerByMagicalPower
            .Select((reindeer, index) => AttachPowerUnit(reindeer, index + 1))
            .ToList();
    }

    private ReindeerPowerUnit AttachPowerUnit(IReindeer reindeer, int indexOfMagicalPower)
        => GeneratePowerUnit(
            reindeer,
            availableAmplifierByMagicalPower.GetValueOrDefault(indexOfMagicalPower, AmplifierType.Basic));

    private static ReindeerPowerUnit GeneratePowerUnit(IReindeer reindeer, AmplifierType amplifierToAttach)
        => new(reindeer, new MagicPowerAmplifier(amplifierToAttach));
}