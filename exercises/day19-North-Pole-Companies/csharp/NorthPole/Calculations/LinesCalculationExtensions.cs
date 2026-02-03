namespace NorthPole;

public static class LinesCalculationExtensions
{
    public static Money Sum(
        this IEnumerable<PrintableInvoice.Line> lines,
        Func<PrintableInvoice.Line, Money> lineAmount)
        => lines.Aggregate(Money.Zero, (sum, line) => sum + lineAmount(line));
}