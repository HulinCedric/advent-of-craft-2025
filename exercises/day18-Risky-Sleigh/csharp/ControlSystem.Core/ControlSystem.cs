using ControlSystem.External;

namespace ControlSystem.Core
{
    public class ControlSystem
    {
        private const int XmasSpirit = 40;
        private readonly Dashboard _dashboard;
        private readonly MagicStable _magicStable = new();
        private readonly List<ReindeerPowerUnit> _reindeerPowerUnits;
        private readonly Dictionary<int, AmplifierType> _availableSpecialAmplifiers = new()
        {
            {1, AmplifierType.Divine},
            {2, AmplifierType.Blessed},
            {3, AmplifierType.Blessed},
        };
        
        public SleighEngineStatus Status { get; private set; }
        public SleighAction Action { get; private set; }
        private float _controlMagicPower = 0;

        public ControlSystem() : this(SleighEngineStatus.Off, SleighAction.Flying)
        {
        }

        public ControlSystem(SleighEngineStatus status, SleighAction action)
        {
            _dashboard = new Dashboard();
            _reindeerPowerUnits = BringAllReindeers();
            Status = status;
            Action = action;
        }

        private List<ReindeerPowerUnit> BringAllReindeers()
        {
            return new BestMagicalPerformancePowerUnitFactory(_magicStable.GetAllReindeers(),
                _availableSpecialAmplifiers).BringAllReindeers();
        }

        public void StartSystem()
        {
            _dashboard.DisplayStatus("Starting the sleigh...");
            Status = SleighEngineStatus.On;
            _dashboard.DisplayStatus("System ready.");
        }

        public void Ascend()
        {
            if (Status == SleighEngineStatus.On)
            {
                foreach (var reindeerPowerUnit in _reindeerPowerUnits)
                {
                    _controlMagicPower += reindeerPowerUnit.HarnessMagicPower();
                }

                if (CheckReindeerStatus())
                {
                    _dashboard.DisplayStatus("Ascending...");
                    Action = SleighAction.Flying;
                    _controlMagicPower = 0;
                }
                else throw new ReindeersNeedRestException();
            }
            else
            {
                throw new SleighNotStartedException();
            }
        }

        public void Descend()
        {
            if (Status == SleighEngineStatus.On)
            {
                _dashboard.DisplayStatus("Descending...");
                Action = SleighAction.Hovering;
            }
            else throw new SleighNotStartedException();
        }

        public void Park()
        {
            if (Status == SleighEngineStatus.On)
            {
                _dashboard.DisplayStatus("Parking...");

                foreach (var reindeerPowerUnit in _reindeerPowerUnits)
                {
                    reindeerPowerUnit.Rest();
                }

                Action = SleighAction.Parked;
            }
            else throw new SleighNotStartedException();
        }

        public void StopSystem()
        {
            _dashboard.DisplayStatus("Stopping the sleigh...");
            Status = SleighEngineStatus.Off;
            _dashboard.DisplayStatus("System shutdown.");
        }

        private bool CheckReindeerStatus()
        {
            return _controlMagicPower >= XmasSpirit;
        }
    }
}