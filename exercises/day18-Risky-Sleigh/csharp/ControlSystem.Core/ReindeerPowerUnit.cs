using ControlSystem.External;

namespace ControlSystem.Core
{
    public class ReindeerPowerUnit
    {
        public Reindeer Reindeer { get; }
        private readonly MagicPowerAmplifier _amplifier;

        public ReindeerPowerUnit(Reindeer reindeer, MagicPowerAmplifier amplifier)
        {
            Reindeer = reindeer;
            _amplifier = amplifier;
        }

        public float HarnessMagicPower()
        {
            if (!Reindeer.NeedsRest())
            {
                Reindeer.IncrementHarnessing();
                return _amplifier.Amplify(Reindeer.GetMagicPower());
            }

            return 0;
        }

        public float CheckMagicPower()
        {
            return Reindeer.GetMagicPower();
        }
    }
}