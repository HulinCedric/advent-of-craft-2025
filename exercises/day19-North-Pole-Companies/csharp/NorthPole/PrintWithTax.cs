using System.Globalization;
using static NorthPole.PrintableInvoice;

namespace NorthPole;

public class PrintWithTax : IPrinter
{
    private readonly CultureInfo _culture = new("en-US");

    public string Print(PrintableInvoice invoice)
        => $"""
            Invoice for {invoice.Customer}
            {Print(invoice.Lines)}
            Subtotal: {invoice.SubTotalAmount.ToString("C", _culture)}
            Total Tax: {invoice.TaxTotalAmount.ToString("C", _culture)}
            Amount owed is {invoice.TotalAmount.ToString("C", _culture)}
            You earned {invoice.LoyaltyPoints} loyalty points

            """;

    private string Print(IReadOnlyList<Line> lines) => string.Join("\n", lines.Select(Print));

    private string Print(Line line)
        => $"""
             {line.CompanyName}: {line.NetAmount.ToString("C", _culture)} ({line.NumberOfPackages} packages)
               Tax ({line.TaxName} - {line.TaxRate.ToString("P0", _culture)}): {line.TaxAmount.ToString("C", _culture)}
            """;
}