namespace ControlSystem.Core;

public interface IReindeer
{
    int TimesHarnessing { get; set; }
    float GetMagicPower();
    bool NeedsRest();
}