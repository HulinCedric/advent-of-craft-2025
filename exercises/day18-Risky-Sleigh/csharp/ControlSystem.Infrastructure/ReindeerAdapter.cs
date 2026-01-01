using ControlSystem.Core;
using ControlSystem.External;

namespace ControlSystem.Infrastructure;

public class ReindeerAdapter(Reindeer externalReindeer) : IReindeer
{
    public int TimesHarnessing
    {
        get => externalReindeer.TimesHarnessing;
        set => externalReindeer.TimesHarnessing = value;
    }

    public float GetMagicPower() => externalReindeer.GetMagicPower();

    public bool NeedsRest() => externalReindeer.NeedsRest();
}