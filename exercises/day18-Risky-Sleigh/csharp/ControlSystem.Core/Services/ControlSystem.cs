using ControlSystem.Core.Modules.Reindeers.Ports;
using ControlSystem.Core.Modules.Sleighs;
using ControlSystem.Core.Ports;

namespace ControlSystem.Core.Services;

public class ControlSystem(Sleigh sleigh, IDashboard dashboard, IPowerUnitFactory powerUnitFactory)
{
    private readonly SleighWithReindeers _sleighWithReindeers = new(sleigh, powerUnitFactory.BringAllReindeers());

    public void StartSystem()
    {
        dashboard.DisplayStatus("Starting the sleigh...");

        _sleighWithReindeers.StartSystem()
            .Do(_ => dashboard.DisplayStatus("System ready."))
            .IfLeft(dashboard.DisplayStatus);
    }

    public void Ascend()
        => _sleighWithReindeers.Ascend()
            .Do(_ => dashboard.DisplayStatus("Ascending..."))
            .IfLeft(dashboard.DisplayStatus);


    public void Descend()
        => _sleighWithReindeers.Descend()
            .Do(_ => dashboard.DisplayStatus("Descending..."))
            .IfLeft(dashboard.DisplayStatus);

    public void Park()
        => _sleighWithReindeers.Park()
            .Do(_ => dashboard.DisplayStatus("Parking..."))
            .IfLeft(dashboard.DisplayStatus);

    public void StopSystem()
    {
        dashboard.DisplayStatus("Stopping the sleigh...");

        _sleighWithReindeers.StopSystem()
            .Do(_ => dashboard.DisplayStatus("System shutdown."))
            .IfLeft(dashboard.DisplayStatus);
    }
}