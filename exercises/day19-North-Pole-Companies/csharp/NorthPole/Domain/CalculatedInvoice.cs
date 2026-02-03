using NorthPole.Calculations;

namespace NorthPole.Domain;

public record CalculatedInvoice(
    string Customer,
    IReadOnlyList<CalculatedInvoice.Line> Lines,
    Money SubTotalAmount,
    Money TaxTotalAmount,
    Money TotalAmount,
    int LoyaltyPoints)
{
    public static CalculatedInvoice Create(EnrichedInvoice invoice, List<Line> lines)
    {
        var subTotalAmount = lines.Sum(line => line.NetAmount);
        var totalTaxAmount = lines.Sum(line => line.TaxAmount());
        var totalAmount = subTotalAmount + totalTaxAmount;
        var totalLoyaltyPoints = lines.Sum(l => l.LoyaltyPoints);

        return new CalculatedInvoice(
            invoice.Customer,
            lines,
            subTotalAmount,
            totalTaxAmount,
            totalAmount,
            totalLoyaltyPoints);
    }

    public record Line(
        int NumberOfPackages,
        string CompanyName,
        TaxLine TaxLine,
        Money NetAmount,
        int LoyaltyPoints)
    {
        public Money TaxAmount() => TaxLine.Amount;
    }
}