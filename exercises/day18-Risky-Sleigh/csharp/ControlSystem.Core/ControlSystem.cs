namespace ControlSystem.Core;

public class ControlSystem
{
    private readonly IDashboard _dashboard;
    private readonly IReadOnlyList<ReindeerPowerUnit> _reindeerPowerUnits;

    private Sleigh _sleigh;

    public ControlSystem(Sleigh sleigh, IDashboard dashboard, IPowerUnitFactory powerUnitFactory)
    {
        _dashboard = dashboard;
        _reindeerPowerUnits = powerUnitFactory.BringAllReindeers();
        _sleigh = sleigh;
    }

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