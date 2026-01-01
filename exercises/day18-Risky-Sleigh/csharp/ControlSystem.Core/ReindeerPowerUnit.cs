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

        public float HarnessMagicPower()
        {
            if (!_reindeer.NeedsRest())
            {
                var magicPower = _amplifier.Amplify(_reindeer.GetMagicPower());
                _reindeer.TimesHarnessing++;
                return magicPower;
            }

            return NoMagicPower;
        }

        public float CheckMagicPower()
        {
            return _reindeer.GetMagicPower();
        }

        public void ReleaseHarness() => _reindeer.TimesHarnessing = NoHarnessing;
    }
}