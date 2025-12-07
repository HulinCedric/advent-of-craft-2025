using static System.Environment;
using static System.Globalization.CultureInfo;
using static System.String;

namespace Day09.Accountability;

public class Client(IReadOnlyDictionary<string, double> orderLines)
{
    public string ToStatement()
        => $"{Join(
            NewLine,
            orderLines
                .Select(kvp => FormatLine(kvp.Key, kvp.Value))
                .ToList()
        )}{NewLine}Total : {TotalAmount().ToString(InvariantCulture)}€";

    private string FormatLine(string name, double value) => name + " for " + value.ToString(InvariantCulture) + "€";

    public double TotalAmount() => orderLines.Sum(kvp => kvp.Value);
}