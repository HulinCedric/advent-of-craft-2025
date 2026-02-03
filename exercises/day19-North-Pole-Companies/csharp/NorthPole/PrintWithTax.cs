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
            Subtotal: {invoice.SubTotalAmount}
            Total Tax: {invoice.TaxTotalAmount}
            Amount owed is {invoice.TotalAmount}
            You earned {invoice.LoyaltyPoints} loyalty points

            """;

    private string Print(IReadOnlyList<Line> lines) => string.Join("\n", lines.Select(Print));

    private string Print(Line line)
        => $"""
             {line.CompanyName}: {line.NetAmount} ({line.NumberOfPackages} packages)
               Tax ({line.TaxName} - {line.TaxRate}): {line.TaxAmount}
            """;
}