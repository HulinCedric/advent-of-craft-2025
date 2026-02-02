using System.Globalization;
using static NorthPole.PrintableInvoice;

namespace NorthPole;

public class PrintWithoutTax : IPrinter
{
    private readonly CultureInfo _culture = new("en-US");

    public string Print(PrintableInvoice invoice)
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