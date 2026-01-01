using ControlSystem.External;

namespace ControlSystem.Core
{
    public class ReindeerPowerUnit
    {
        private const float NoMagicPower = 0;
        private const int NoHarnessing = 0;

        private readonly Reindeer _reindeer;
        private readonly MagicPowerAmplifier _amplifier;

        public ReindeerPowerUnit(Reindeer reindeer, MagicPowerAmplifier amplifier)
        {
            _reindeer = reindeer;
            _amplifier = amplifier;
        }

        public void HarnessMagicPower()
        {
            if (_reindeer.NeedsRest()) return;

            _reindeer.TimesHarnessing++;
        }

        public float CheckMagicPower()
            => _reindeer.NeedsRest() ? NoMagicPower : _amplifier.Amplify(_reindeer.GetMagicPower());

        public void ReleaseHarness() => _reindeer.TimesHarnessing = NoHarnessing;
    }
}