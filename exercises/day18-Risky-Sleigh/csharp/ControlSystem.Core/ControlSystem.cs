using ControlSystem.External;

namespace ControlSystem.Core;

public class ControlSystem
{
    private const int RequiredMagicPowerForAscend = 40;

    private const string SleighNotStartedFailure =
        "The sleigh is not started. Please start the sleigh before any other action...";

    private const string ReindeersNeedRestFailure = "The reindeer needs rest. Please park the sleigh...";

    private readonly Dictionary<int, AmplifierType> _availableSpecialAmplifiers = new()
    {
        { 1, AmplifierType.Divine },
        { 2, AmplifierType.Blessed },
        { 3, AmplifierType.Blessed }
    };

    private readonly MagicStable _magicStable = new();
    private readonly List<ReindeerPowerUnit> _reindeerPowerUnits;

    private readonly Sleigh _sleigh;

    public ControlSystem(Sleigh sleigh)
    {
        Dashboard = new Dashboard();
        _reindeerPowerUnits = BringAllReindeers();
        _sleigh = sleigh;
    }

    public SleighEngineStatus Status => _sleigh.Status;
    public SleighAction Action => _sleigh.Action;

    public Dashboard Dashboard { get; }

    private List<ReindeerPowerUnit> BringAllReindeers()
        => new BestMagicalPerformancePowerUnitFactory(
            _magicStable.GetAllReindeers(),
            _availableSpecialAmplifiers).BringAllReindeers();

    public void StartSystem()
    {
        Dashboard.DisplayStatus("Starting the sleigh...");

        _sleigh.TurnOn()
            .Match(
                _ => Dashboard.DisplayStatus("System ready."),
                failure => Dashboard.DisplayStatus(failure));
    }

    public void Ascend()
    {
        if (_sleigh.Status != SleighEngineStatus.On)
        {
            Dashboard.DisplayStatus(SleighNotStartedFailure);
            return;
        }

        var availableMagicPower = _reindeerPowerUnits.Sum(reindeerPowerUnit => reindeerPowerUnit.CheckMagicPower());
        if (availableMagicPower < RequiredMagicPowerForAscend)
        {
            Dashboard.DisplayStatus(ReindeersNeedRestFailure);
            return;
        }

        foreach (var reindeerPowerUnit in _reindeerPowerUnits)
        {
            reindeerPowerUnit.HarnessMagicPower();
        }

        Dashboard.DisplayStatus("Ascending...");
        _sleigh.Action = SleighAction.Flying;
    }

    public void Descend()
        => _sleigh.Descend()
            .Match(
                _ => Dashboard.DisplayStatus("Descending..."),
                failure => Dashboard.DisplayStatus(failure));

    public void Park()
    {
        if (_sleigh.Status != SleighEngineStatus.On)
        {
            Dashboard.DisplayStatus(SleighNotStartedFailure);
            return;
        }

        Dashboard.DisplayStatus("Parking...");

        foreach (var reindeerPowerUnit in _reindeerPowerUnits)
        {
            reindeerPowerUnit.ReleaseHarness();
        }

        _sleigh.Action = SleighAction.Parked;
    }

    public void StopSystem()
    {
        Dashboard.DisplayStatus("Stopping the sleigh...");
        _sleigh.TurnOff()
            .Match(
                _ => Dashboard.DisplayStatus("System shutdown."),
                failure => Dashboard.DisplayStatus(failure));
    }
}