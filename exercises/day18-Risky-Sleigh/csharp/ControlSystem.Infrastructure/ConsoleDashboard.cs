using ControlSystem.Core;
using ControlSystem.Core.Ports;

namespace ControlSystem.Infrastructure;

public class ConsoleDashboard : IDashboard
{
    public void DisplayStatus(string message) => Console.WriteLine(message);
}