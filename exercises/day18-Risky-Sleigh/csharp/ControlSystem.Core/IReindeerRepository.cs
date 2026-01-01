using ControlSystem.Core;

namespace ControlSystem.Tests;

public interface IReindeerRepository
{
    IReadOnlyList<IReindeer> GetAllReindeers();
}