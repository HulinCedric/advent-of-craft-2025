using static NorthPole.CalculatedInvoice;

namespace NorthPole.Printers;

public class PrintWithTax : IPrinter
{
    public string Print(CalculatedInvoice invoice)
        => $"""
            Invoice for {invoice.Customer}
            {Print(invoice.Lines)}
            Subtotal: {invoice.SubTotalAmount}
            Total Tax: {invoice.TaxTotalAmount}
            Amount owed is {invoice.TotalAmount}
            You earned {invoice.LoyaltyPoints} loyalty points

            """;

    private static string Print(IReadOnlyList<Line> lines) => string.Join("\n", lines.Select(Print));

    private static string Print(Line line)
        => $"""
             {line.CompanyName}: {line.NetAmount} ({line.NumberOfPackages} packages)
               {line.TaxLine}
            """;
}