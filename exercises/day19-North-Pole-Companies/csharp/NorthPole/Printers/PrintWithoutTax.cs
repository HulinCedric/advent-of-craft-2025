using static NorthPole.CalculatedInvoice;

namespace NorthPole;

public class PrintWithoutTax : IPrinter
{
    public string Print(CalculatedInvoice invoice)
        => $"""
            Invoice for {invoice.Customer}
            {Print(invoice.Lines)}
            Amount owed is {invoice.TotalAmount}
            You earned {invoice.LoyaltyPoints} loyalty points

            """;

    private static string Print(IReadOnlyList<Line> lines) => string.Join("\n", lines.Select(Print));

    private static string Print(Line line)
        => $" {line.CompanyName}: {line.NetAmount} ({line.NumberOfPackages} packages)";
}