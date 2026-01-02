using ControlSystem.Core.Modules.Reindeers;
using ControlSystem.Core.Modules.Reindeers.Ports;
using ControlSystem.Core.Modules.Sleighs;
using ControlSystem.Core.Ports;
using LanguageExt;

namespace ControlSystem.Core.Services;

public class ControlSystem(Sleigh sleigh, IDashboard dashboard, IPowerUnitFactory powerUnitFactory)
{
    private readonly HarnessedReindeers _reindeerPowerUnits = powerUnitFactory.BringAllReindeers();

    private Sleigh _sleigh = sleigh;

    public void StartSystem()
    {
        dashboard.DisplayStatus("Starting the sleigh...");

        _sleigh.TurnOn()
            .Do(sleigh => _sleigh = sleigh)
            .Do(_ => dashboard.DisplayStatus("System ready."))
            .IfLeft(dashboard.DisplayStatus);
    }

    public void Ascend()
        => AscendSleigh()
            .Do(sleigh => _sleigh = sleigh)
            .Do(_ => dashboard.DisplayStatus("Ascending..."))
            .IfLeft(dashboard.DisplayStatus);

    private Either<string, Sleigh> AscendSleigh()
        => from sleigh in _sleigh.Ascend()
            from _ in _reindeerPowerUnits.HarnessAllPower()
            select sleigh;

    public void Descend()
        => _sleigh.Descend()
            .Do(sleigh => _sleigh = sleigh)
            .Do(_ => dashboard.DisplayStatus("Descending..."))
            .IfLeft(dashboard.DisplayStatus);

    public void Park()
        => _sleigh.Park()
            .Do(sleigh => _sleigh = sleigh)
            .Do(_ => _reindeerPowerUnits.RestReindeers())
            .Do(_ => dashboard.DisplayStatus("Parking..."))
            .IfLeft(dashboard.DisplayStatus);

    public void StopSystem()
    {
        dashboard.DisplayStatus("Stopping the sleigh...");

        _sleigh.TurnOff()
            .Do(sleigh => _sleigh = sleigh)
            .Do(_ => dashboard.DisplayStatus("System shutdown."))
            .IfLeft(dashboard.DisplayStatus);
    }
}