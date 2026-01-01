using ControlSystem.Core;
using ControlSystem.External;
using ControlSystem.Tests;

namespace ControlSystem.Infrastructure;

public class ReindeerRepository : IReindeerRepository
{
    public IReadOnlyList<IReindeer> GetAllReindeers() => new MagicStable()
        .GetAllReindeers()
        .Select(r => new ReindeerAdapter(r))
        .ToList();
}