namespace ControlSystem.Core.Models.Reindeers.Ports;

public interface IReindeerRepository
{
    IReadOnlyList<IReindeer> GetAllReindeers();
}