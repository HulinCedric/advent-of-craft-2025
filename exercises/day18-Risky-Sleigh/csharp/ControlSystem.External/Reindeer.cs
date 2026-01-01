namespace ControlSystem.External
{
    public class Reindeer
    {
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
            _powerPullLimit = age <= 5
                ? 5
                : 5 - (age - 5);
        }

        public float GetMagicPower()
        {
            if (!_sick && !NeedsRest())
            {
                if (_age == 1)
                    return _spirit * 0.5f;
                if (_age <= 5)
                    return _spirit;
                return _spirit * 0.25f;
            }

            return 0;
        }


        public bool NeedsRest()
        {
            if (!_sick) return _timesHarnessing == _powerPullLimit;

            return true;
        }

        public void IncrementHarnessing() => _timesHarnessing++;
        public void Rest() => _timesHarnessing = 0;
    }
}