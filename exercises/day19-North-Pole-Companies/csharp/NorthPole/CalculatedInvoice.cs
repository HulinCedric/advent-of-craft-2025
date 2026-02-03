namespace NorthPole;

public record CalculatedInvoice(
    string Customer,
    IReadOnlyList<CalculatedInvoice.Line> Lines,
    Money SubTotalAmount,
    Money TaxTotalAmount,
    Money TotalAmount,
    int LoyaltyPoints)
{
    public record Line(
        int NumberOfPackages,
        string CompanyName,
        TaxLine TaxLine,
        Money NetAmount,
        Money GrossAmount,
        int LoyaltyPoints)
    {
        public Money TaxAmount() => TaxLine.Amount;
    }
}