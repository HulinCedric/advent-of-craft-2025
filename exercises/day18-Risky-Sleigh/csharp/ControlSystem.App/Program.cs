using ControlSystem.Infrastructure;

namespace ControlSystem
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var reindeerRepository = new ReindeerRepository();
            
            var availableSpecialAmplifiers = new Dictionary<int, AmplifierType>()
            {
                { 1, AmplifierType.Divine },
                { 2, AmplifierType.Blessed },
                { 3, AmplifierType.Blessed }
            };

            var powerUnitFactory = new BestMagicalPerformancePowerUnitFactory(
                reindeerRepository.GetAllReindeers(),
                availableSpecialAmplifiers);
            
            var controlSystem = new ControlSystem.Core.ControlSystem(Sleigh.New(), new ConsoleDashboard(), powerUnitFactory);
            controlSystem.StartSystem();

            var keepRunning = true;

            while (keepRunning)
            {
                Console.WriteLine("Enter a command (ascend (a), descend (d), park (p), or quit (q)): ");
                var command = Console.ReadLine();

                switch (command)
                {
                    case "ascend":
                    case "a":
                        controlSystem.Ascend();

                        break;

                    case "descend":
                    case "d":
                        controlSystem.Descend();

                        break;

                    case "park":
                    case "p":
                        controlSystem.Park();

                        break;

                    case "quit":
                    case "q":
                        keepRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }

            controlSystem.StopSystem();
        }
    }
}