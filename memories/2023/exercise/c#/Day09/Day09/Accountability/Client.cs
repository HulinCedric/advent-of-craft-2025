using static System.Environment;
using static System.Globalization.CultureInfo;
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

        if (!IsNullOrEmpty(result)) result += NewLine;

        result += $"Total : {TotalAmount().ToString("0.##", InvariantCulture)}€";

        return result;
    }

    private string FormatLine(string name, double value) => $"{name} for {value.ToString("0.##", InvariantCulture)}€";

    public double TotalAmount() => _orderLines.Sum(kvp => kvp.Value);
}