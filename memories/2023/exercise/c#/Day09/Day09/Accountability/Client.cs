using System.Collections.Immutable;
using static System.Environment;
using static System.Globalization.CultureInfo;
using static System.String;

namespace Day09.Accountability;

public class Client
{
    private readonly IReadOnlyDictionary<string, double> _orderLines;

    public Client(IReadOnlyDictionary<string, double> orderLines) => _orderLines = orderLines.ToDictionary();

    public string ToStatement()
        => $"{Join(
            NewLine,
            _orderLines
                .Select(kvp => FormatLine(kvp.Key, kvp.Value))
                .ToList()
        )}{NewLine}Total : {TotalAmount().ToString(InvariantCulture)}€";

    private string FormatLine(string name, double value) => name + " for " + value.ToString(InvariantCulture) + "€";

    public double TotalAmount() => _orderLines.Sum(kvp => kvp.Value);
}