using static System.Environment;
using static System.FormattableString;
using static System.String;

namespace Day09.Accountability;

public class Client
{
    private readonly IReadOnlyDictionary<string, double> _orderLines;

    public Client(IReadOnlyDictionary<string, double> orderLines)
    {
        ArgumentNullException.ThrowIfNull(orderLines);

        _orderLines = orderLines.ToDictionary();
    }

    public string ToStatement()
    {
        var result = $"{Join(
            NewLine,
            _orderLines
                .Select(kvp => FormatLine(kvp.Key, kvp.Value))
        )}";

        if (_orderLines.Any()) result += NewLine;

        result += $"Total : {FormatAmount(TotalAmount())}";

        return result;
    }

    private static string FormatLine(string name, double value) => $"{name} for {FormatAmount(value)}";

    private static string FormatAmount(double value) => Invariant($"{value:0.##}€");

    public double TotalAmount() => _orderLines.Sum(kvp => kvp.Value);
}