using System.Text;
using ControlSystem.Core;
using ControlSystem.Core.Ports;

namespace ControlSystem.Tests.TestDoubles;

public class SpyDashboard : IDashboard
{
    private readonly StringBuilder _output = new();

    public void DisplayStatus(string message) => _output.AppendLine(message);
    public string Output() => _output.ToString().Trim();
}