namespace NorthPole;

public record PrintableInvoice(
    string Customer,
    IReadOnlyList<PrintableInvoice.Line> Lines,
    Money SubTotalAmount,
    Money TaxTotalAmount,
    Money TotalAmount,
    int LoyaltyPoints)
{
    public record Line(
        int NumberOfPackages,
        string CompanyName,
        string TaxName,
        TaxRate TaxRate,
        Money NetAmount,
        Money TaxAmount,
        Money GrossAmount,
        int LoyaltyPoints);
}