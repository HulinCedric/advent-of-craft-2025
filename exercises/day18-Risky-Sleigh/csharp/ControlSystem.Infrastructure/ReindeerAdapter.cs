using ControlSystem.Core;
using ControlSystem.Core.Models.Reindeers.Ports;
using ControlSystem.Core.Ports;
using ControlSystem.External;

namespace ControlSystem.Infrastructure;

public class ReindeerAdapter(Reindeer externalReindeer) : IReindeer
{
    private const int NoHarnessing = 0;

    public float GetMagicPower() => externalReindeer.GetMagicPower();

    public bool NeedsRest() => externalReindeer.NeedsRest();

    public void Rest() => externalReindeer.TimesHarnessing = NoHarnessing;

    public void Harness() => externalReindeer.TimesHarnessing++;
}