using ControlSystem.External;

namespace ControlSystem.Core;

public class ControlSystem
{
    private readonly Dictionary<int, AmplifierType> _availableSpecialAmplifiers = new()
    {
        { 1, AmplifierType.Divine },
        { 2, AmplifierType.Blessed },
        { 3, AmplifierType.Blessed }
    };

    private readonly Dashboard _dashboard;

    private readonly MagicStable _magicStable = new();
    private readonly List<ReindeerPowerUnit> _reindeerPowerUnits;

    private Sleigh _sleigh;

    public ControlSystem(Sleigh sleigh)
    {
        _dashboard = new Dashboard();
        _reindeerPowerUnits = BringAllReindeers();
        _sleigh = sleigh;
    }

    public SleighAction Action => _sleigh.Action;

    private List<ReindeerPowerUnit> BringAllReindeers()
        => new BestMagicalPerformancePowerUnitFactory(
            _magicStable.GetAllReindeers(),
            _availableSpecialAmplifiers).BringAllReindeers();

    public void StartSystem()
    {
        _dashboard.DisplayStatus("Starting the sleigh...");

        _sleigh.TurnOn()
            .Match(
                updatedSleigh =>
                {
                    _sleigh = updatedSleigh;
                    
                    _dashboard.DisplayStatus("System ready.");
                },
                failure => _dashboard.DisplayStatus(failure));
    }

    public void Ascend()
    {
        var availableMagicPower = _reindeerPowerUnits.Sum(reindeerPowerUnit => reindeerPowerUnit.CheckMagicPower());
        _sleigh.Ascend(availableMagicPower)
            .Match(
                updatedSleigh =>
                {
                    _sleigh = updatedSleigh;
                    
                    _dashboard.DisplayStatus("Ascending...");

                    foreach (var reindeerPowerUnit in _reindeerPowerUnits)
                    {
                        reindeerPowerUnit.HarnessMagicPower();
                    }
                },
                failure => _dashboard.DisplayStatus(failure));
    }

    public void Descend()
        => _sleigh.Descend()
            .Match(
                updatedSleigh =>
                {
                    _sleigh = updatedSleigh;
                    
                    _dashboard.DisplayStatus("Descending...");
                },
                failure => _dashboard.DisplayStatus(failure));

    public void Park()
        => _sleigh.Park()
            .Match(
                updatedSleigh =>
                {
                    _sleigh = updatedSleigh;
                    
                    _dashboard.DisplayStatus("Parking...");

                    foreach (var reindeerPowerUnit in _reindeerPowerUnits)
                    {
                        reindeerPowerUnit.ReleaseHarness();
                    }
                },
                failure => _dashboard.DisplayStatus(failure));

    public void StopSystem()
    {
        _dashboard.DisplayStatus("Stopping the sleigh...");
        _sleigh.TurnOff()
            .Match(
                updatedSleigh =>
                {
                    _sleigh = updatedSleigh;
                    
                    _dashboard.DisplayStatus("System shutdown.");
                },
                failure => _dashboard.DisplayStatus(failure));
    }
}