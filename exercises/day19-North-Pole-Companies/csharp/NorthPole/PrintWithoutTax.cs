using System.Globalization;
using static NorthPole.PrintableInvoice;

namespace NorthPole;

public class TextPrinter : IPrinter
{
    private readonly CultureInfo _culture = new("en-US");

    public string Print(PrintableInvoice invoice)
        => $"""
            Invoice for {invoice.Customer}
            {Print(invoice.Lines)}
            Amount owed is {invoice.TotalAmount.ToString("C", _culture)}
            You earned {invoice.LoyaltyPoints} loyalty points

            """;

    private string Print(IReadOnlyList<Line> lines) => string.Join("\n", lines.Select(Print));

    private string Print(Line line)
        => $" {line.CompanyName}: {line.GrossAmount.ToString("C", _culture)} ({line.NumberOfPackages} packages)";
}