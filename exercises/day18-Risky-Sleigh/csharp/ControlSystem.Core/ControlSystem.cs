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
            if (Status == SleighEngineStatus.On) return;
            
            _dashboard.DisplayStatus("Starting the sleigh...");
            Status = SleighEngineStatus.On;
            _dashboard.DisplayStatus("System ready.");
        }

        public void Ascend()
        {
            if (Status != SleighEngineStatus.On)
                throw new SleighNotStartedException();
            
            var availableMagicPower = _reindeerPowerUnits.Sum(reindeerPowerUnit => reindeerPowerUnit.CheckMagicPower());
            if (availableMagicPower < XmasSpirit)
            {
                _dashboard.DisplayStatus("The reindeer needs rest. Please park the sleigh...");
                return;
            }

            foreach (var reindeerPowerUnit in _reindeerPowerUnits)
            {
                reindeerPowerUnit.HarnessMagicPower();
            }

            _dashboard.DisplayStatus("Ascending...");
            Action = SleighAction.Flying;
        }

        public void Descend()
        {
            if (Status != SleighEngineStatus.On)
                throw new SleighNotStartedException();
            
            if (Action != SleighAction.Flying) return;

            _dashboard.DisplayStatus("Descending...");
            Action = SleighAction.Hovering;
        }

        public void Park()
        {
            if (Status != SleighEngineStatus.On)
                throw new SleighNotStartedException();
            
            _dashboard.DisplayStatus("Parking...");

            foreach (var reindeerPowerUnit in _reindeerPowerUnits)
            {
                reindeerPowerUnit.ReleaseHarness();
            }

            Action = SleighAction.Parked;
        }

        public void StopSystem()
        {
            if (Status == SleighEngineStatus.Off) return;
            
            _dashboard.DisplayStatus("Stopping the sleigh...");
            Status = SleighEngineStatus.Off;
            _dashboard.DisplayStatus("System shutdown.");
        }
    }
}