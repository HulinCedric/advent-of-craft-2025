using System.Text;
using ControlSystem.Core;

namespace ControlSystem.Tests;

public class SpyDashboard : IDashboard
{
    private readonly StringBuilder _output = new();

    public void DisplayStatus(string message) => _output.AppendLine(message);
    public string Output() => _output.ToString().Trim();
}