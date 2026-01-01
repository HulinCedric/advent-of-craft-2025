namespace ControlSystem.Core;

public class ControlSystem(Sleigh sleigh, IDashboard dashboard, IPowerUnitFactory powerUnitFactory)
{
    private readonly IReadOnlyList<ReindeerPowerUnit> _reindeerPowerUnits = powerUnitFactory.BringAllReindeers();

    private Sleigh _sleigh = sleigh;

    public void StartSystem()
    {
        dashboard.DisplayStatus("Starting the sleigh...");

        _sleigh.TurnOn()
            .Match(
                updatedSleigh =>
                {
                    _sleigh = updatedSleigh;

                    dashboard.DisplayStatus("System ready.");
                },
                dashboard.DisplayStatus);
    }

    public void Ascend()
    {
        var availableMagicPower = _reindeerPowerUnits.Sum(reindeerPowerUnit => reindeerPowerUnit.CheckMagicPower());
        _sleigh.Ascend(availableMagicPower)
            .Match(
                updatedSleigh =>
                {
                    _sleigh = updatedSleigh;

                    dashboard.DisplayStatus("Ascending...");

                    foreach (var reindeerPowerUnit in _reindeerPowerUnits)
                    {
                        reindeerPowerUnit.HarnessMagicPower();
                    }
                },
                dashboard.DisplayStatus);
    }

    public void Descend()
        => _sleigh.Descend()
            .Match(
                updatedSleigh =>
                {
                    _sleigh = updatedSleigh;

                    dashboard.DisplayStatus("Descending...");
                },
                dashboard.DisplayStatus);

    public void Park()
        => _sleigh.Park()
            .Match(
                updatedSleigh =>
                {
                    _sleigh = updatedSleigh;

                    dashboard.DisplayStatus("Parking...");

                    foreach (var reindeerPowerUnit in _reindeerPowerUnits)
                    {
                        reindeerPowerUnit.ReleaseHarness();
                    }
                },
                dashboard.DisplayStatus);

    public void StopSystem()
    {
        dashboard.DisplayStatus("Stopping the sleigh...");
        _sleigh.TurnOff()
            .Match(
                updatedSleigh =>
                {
                    _sleigh = updatedSleigh;

                    dashboard.DisplayStatus("System shutdown.");
                },
                dashboard.DisplayStatus);
    }
}