using ControlSystem.Infrastructure;
using FluentAssertions;

namespace ControlSystem.Tests;

public sealed class ConsoleDashboardShould : IDisposable
{
    private readonly ConsoleDashboard _dashboard;
    private readonly TextWriter _originalOutput;
    private readonly StringWriter _output;

    public ConsoleDashboardShould()
    {
        _output = new StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_output);

        _dashboard = new ConsoleDashboard();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            Console.SetOut(_originalOutput);
            _output.Dispose();
        }
    }

    ~ConsoleDashboardShould() => Dispose(false);

    [Fact]
    public void Write_into_console()
    {
        _dashboard.DisplayStatus("Hello, Dashboard!");

        _output.ToString()
            .Trim()
            .Should()
            .Be("Hello, Dashboard!");
    }
}