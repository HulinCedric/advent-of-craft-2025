namespace ControlSystem.External
{
    public class Reindeer
    {
        private const float NoMagicPower = 0;
        private const int NoHarnessing = 0;

        private const int AdultAge = 5;
        private const int YoungAge = 1;
        
        private const int FullPower = 5;

        private readonly int _age;
        private readonly int _powerPullLimit;
        private readonly bool _sick;
        private readonly int _spirit;

        private int _timesHarnessing;

        public Reindeer(string name, int age, int spirit, bool sick = false)
        {
            _age = age;
            _spirit = spirit;
            _sick = sick;
            _powerPullLimit = age <= AdultAge
                ? FullPower
                : FullPower - (age - AdultAge);
        }

        public float GetMagicPower()
        {
            if (_sick) return NoMagicPower;
            
            if (NeedsRest()) return NoMagicPower;

            if (IsYoung())
                return HalfSpirit();

            if (IsAdult())
                return FullSpirit();

            return QuarterSpirit();
        }

        private bool IsYoung() => _age <= YoungAge;
        private bool IsAdult() => _age is > YoungAge and <= AdultAge;
        private bool IsOld() => _age > AdultAge;

        private int FullSpirit() => _spirit;
        private float HalfSpirit() => _spirit * 0.5f;
        private float QuarterSpirit() => _spirit * 0.25f;

        public bool NeedsRest()
        {
            if (!_sick) return _timesHarnessing == _powerPullLimit;

            return true;
        }

        public void IncrementHarnessing() => _timesHarnessing++;

        public void Rest() => _timesHarnessing = NoHarnessing;
    }
}