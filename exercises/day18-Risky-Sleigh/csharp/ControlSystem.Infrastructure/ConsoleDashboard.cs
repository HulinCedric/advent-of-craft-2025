using ControlSystem.Core;

namespace ControlSystem.Infrastructure;

public class ConsoleDashboard : IDashboard
{
    public void DisplayStatus(string message) => Console.WriteLine(message);
}