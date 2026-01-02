using ControlSystem.Core;
using ControlSystem.Core.Modules.Reindeers.Ports;
using ControlSystem.Core.Ports;
using ControlSystem.External;

namespace ControlSystem.Infrastructure;

public class ReindeerRepository : IReindeerRepository
{
    public IReadOnlyList<IReindeer> GetAllReindeers() => new MagicStable()
        .GetAllReindeers()
        .Select(r => new ReindeerAdapter(r))
        .ToList();
}