namespace ControlSystem.Core.Modules.Reindeers.Ports;

public interface IReindeerRepository
{
    IReadOnlyList<IReindeer> GetAllReindeers();
}