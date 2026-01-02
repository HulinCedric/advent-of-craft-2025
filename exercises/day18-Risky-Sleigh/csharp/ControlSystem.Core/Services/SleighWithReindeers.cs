using ControlSystem.Core.Modules.Reindeers;
using ControlSystem.Core.Modules.Sleighs;
using LanguageExt;

namespace ControlSystem.Core.Services;

public class SleighWithReindeers(Sleigh sleigh, HarnessedReindeers reindeers)
{
    private Sleigh _sleigh = sleigh;

    public Either<string, Unit> StartSystem()
        => _sleigh.TurnOn()
            .Do(sleigh => _sleigh = sleigh)
            .Map(_ => Unit.Default);

    public Either<string, Unit> Ascend()
        => AscendSleigh()
            .Do(sleigh => _sleigh = sleigh)
            .Map(_ => Unit.Default);

    private Either<string, Sleigh> AscendSleigh()
        => from sleigh in _sleigh.Ascend()
            from _ in reindeers.HarnessAllPower()
            select sleigh;

    public Either<string, Unit> Descend()
        => _sleigh.Descend()
            .Do(sleigh => _sleigh = sleigh)
            .Map(_ => Unit.Default);

    public Either<string, Unit> Park()
        => _sleigh.Park()
            .Do(sleigh => _sleigh = sleigh)
            .Do(_ => reindeers.RestReindeers())
            .Map(_ => Unit.Default);

    public Either<string, Unit> StopSystem()
        => _sleigh.TurnOff()
            .Do(sleigh => _sleigh = sleigh)
            .Map(_ => Unit.Default);
}