using ControlSystem.External;

namespace ControlSystem.Core
{
    public class ReindeerPowerUnit
    {
        private readonly Reindeer _reindeer;
        private readonly MagicPowerAmplifier _amplifier;

        public ReindeerPowerUnit(Reindeer reindeer, MagicPowerAmplifier amplifier)
        {
            _reindeer = reindeer;
            _amplifier = amplifier;
        }

        public float HarnessMagicPower()
        {
            if (!_reindeer.NeedsRest())
            {
                _reindeer.IncrementHarnessing();
                return _amplifier.Amplify(_reindeer.GetMagicPower());
            }

            return 0;
        }

        public float CheckMagicPower()
        {
            return _reindeer.GetMagicPower();
        }

        public void ReleaseHarness() => _reindeer.Rest();
    }
}