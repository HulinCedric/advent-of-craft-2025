namespace ControlSystem.Core;

public class ReindeerPowerUnit(IReindeer reindeer, MagicPowerAmplifier amplifier)
{
    private const float NoMagicPower = 0;

    public void HarnessMagicPower()
    {
        if (reindeer.NeedsRest()) return;

        reindeer.Harness();
    }

    public float CheckMagicPower()
        => reindeer.NeedsRest()
            ? NoMagicPower
            : amplifier.Amplify(reindeer.GetMagicPower());

    public void ReleaseHarness() => reindeer.Rest();
}