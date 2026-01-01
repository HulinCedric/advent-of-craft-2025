namespace ControlSystem.Core;

public class BestMagicalPerformancePowerUnitFactory : IPowerUnitFactory {
    private IReadOnlyList<IReindeer> allReindeers;
    private Dictionary<int, AmplifierType> availableAmplifierByMagicalPower;

    public BestMagicalPerformancePowerUnitFactory(IReadOnlyList<IReindeer> allReindeers, Dictionary<int, AmplifierType> availableAmplifierByMagicalPower) {
        this.allReindeers = allReindeers;
        this.availableAmplifierByMagicalPower = availableAmplifierByMagicalPower;
    }

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
    {
        return GeneratePowerUnit(
            reindeer, 
            availableAmplifierByMagicalPower.GetValueOrDefault(indexOfMagicalPower, AmplifierType.Basic)
        );
    }
    
    private ReindeerPowerUnit GeneratePowerUnit(IReindeer reindeer, AmplifierType amplifierToAttach)
    {
        return new ReindeerPowerUnit(reindeer, new MagicPowerAmplifier(amplifierToAttach));
    }
}