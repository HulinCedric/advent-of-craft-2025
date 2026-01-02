namespace ControlSystem.Core.Models.Reindeers.Ports;

public interface IReindeer
{
    float GetMagicPower();
    bool NeedsRest();
    void Rest();
    void Harness();
}