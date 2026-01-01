namespace ControlSystem.Core;

public interface IReindeer
{
    float GetMagicPower();
    bool NeedsRest();
    void Rest();
    void Harness();
}