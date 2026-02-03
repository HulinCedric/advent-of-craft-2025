using NorthPole.Domain;

namespace NorthPole.Calculations;

public static class LinesCalculationExtensions
{
    public static Money Sum(
        this IEnumerable<CalculatedInvoice.Line> lines,
        Func<CalculatedInvoice.Line, Money> lineAmount)
        => lines.Aggregate(Money.Zero, (sum, line) => sum + lineAmount(line));
}