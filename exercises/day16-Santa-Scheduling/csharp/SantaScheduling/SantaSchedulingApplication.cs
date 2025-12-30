namespace SantaScheduling;

public static class SantaSchedulingApplication
{
    public static void Run(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: SantaScheduling <command> <timezone>");
            Console.WriteLine("Commands:");
            Console.WriteLine("  a - Show arrival time");
            Console.WriteLine("  l - Show departure time");
            Console.WriteLine("Example: SantaScheduling a -5");
            return;
        }

        var cmd = args[0];
        var tz = double.Parse(args[1]);

        if (cmd == "a")
        {
            // NOTE: Your task is to document THIS arrival calculation only
            var arrival = SantaSchedulingArrivalCommand.ComputeArrival(tz);
            Console.WriteLine($"Santa arrives: {arrival}");
        }
        else if (cmd == "l")
        {
            // KLAUS SAYS: DO NOT TOUCH! Departure logic is still being used by North Pole systems.
            // You only need to understand arrival times for now.
            var departure = new DateTime(
                2024,
                12,
                25 + (tz < -5 ? 1 : 0),
                tz < 0 ? 4 : 2,
                0,
                0);
            Console.WriteLine($"Santa departs: {departure}");
        }
        else
        {
            Console.WriteLine($"Unknown command: {cmd}");
        }
    }
}