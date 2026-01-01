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

    private readonly Dashboard _dashboard;

    private readonly MagicStable _magicStable = new();
    private readonly List<ReindeerPowerUnit> _reindeerPowerUnits;

    private readonly Sleigh _sleigh;

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
                _ => _dashboard.DisplayStatus("System ready."),
                failure => _dashboard.DisplayStatus(failure));
    }

    public void Ascend()
    {
        if (_sleigh.Status != SleighEngineStatus.On)
        {
            _dashboard.DisplayStatus(SleighNotStartedFailure);
            return;
        }

        var availableMagicPower = _reindeerPowerUnits.Sum(reindeerPowerUnit => reindeerPowerUnit.CheckMagicPower());
        if (availableMagicPower < RequiredMagicPowerForAscend)
        {
            _dashboard.DisplayStatus(ReindeersNeedRestFailure);
            return;
        }

        foreach (var reindeerPowerUnit in _reindeerPowerUnits)
        {
            reindeerPowerUnit.HarnessMagicPower();
        }

        _dashboard.DisplayStatus("Ascending...");
        _sleigh.Action = SleighAction.Flying;
    }

    public void Descend()
        => _sleigh.Descend()
            .Match(
                _ => _dashboard.DisplayStatus("Descending..."),
                failure => _dashboard.DisplayStatus(failure));

    public void Park()
        => _sleigh.Park()
            .Match(
                _ =>
                {
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
                _ => _dashboard.DisplayStatus("System shutdown."),
                failure => _dashboard.DisplayStatus(failure));
    }
}