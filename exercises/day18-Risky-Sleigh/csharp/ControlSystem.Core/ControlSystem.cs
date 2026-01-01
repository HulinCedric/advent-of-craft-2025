namespace ControlSystem.Core;

public class ControlSystem(Sleigh sleigh, IDashboard dashboard, IPowerUnitFactory powerUnitFactory)
{
    private readonly HarnessedReindeers _reindeerPowerUnits = powerUnitFactory.BringAllReindeers();

    private Sleigh _sleigh = sleigh;

    public void StartSystem()
    {
        dashboard.DisplayStatus("Starting the sleigh...");

        _sleigh.TurnOn()
            .Match(
                sleigh =>
                {
                    _sleigh = sleigh;

                    dashboard.DisplayStatus("System ready.");
                },
                dashboard.DisplayStatus);
    }

    public void Ascend()
        => (from sleigh in _sleigh.Ascend()
                from _ in _reindeerPowerUnits.HarnessAllPower()
                select sleigh)
            .Match(
                sleigh =>
                {
                    _sleigh = sleigh;

                    dashboard.DisplayStatus("Ascending...");
                },
                dashboard.DisplayStatus);

    public void Descend()
        => _sleigh.Descend()
            .Match(
                sleigh =>
                {
                    _sleigh = sleigh;

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

                    _reindeerPowerUnits.RestReindeers();
                },
                dashboard.DisplayStatus);

    public void StopSystem()
    {
        dashboard.DisplayStatus("Stopping the sleigh...");

        _sleigh.TurnOff()
            .Match(
                sleigh =>
                {
                    _sleigh = sleigh;

                    dashboard.DisplayStatus("System shutdown.");
                },
                dashboard.DisplayStatus);
    }
}