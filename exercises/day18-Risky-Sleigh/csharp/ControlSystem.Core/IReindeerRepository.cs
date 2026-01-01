namespace ControlSystem.Core;

public interface IReindeerRepository
{
    IReadOnlyList<IReindeer> GetAllReindeers();
}