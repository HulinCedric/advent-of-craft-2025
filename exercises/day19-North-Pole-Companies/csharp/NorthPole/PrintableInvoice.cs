namespace NorthPole;

public record PrintableInvoice(
    string Customer,
    IReadOnlyList<PrintableInvoice.Line> Lines,
    decimal SubTotalAmount,
    decimal TaxTotalAmount,
    decimal TotalAmount,
    int LoyaltyPoints)
{
    public record Line(
        int NumberOfPackages,
        string CompanyName,
        string TaxName,
        decimal TaxRate,
        Money NetAmount,
        decimal TaxAmount,
        decimal GrossAmount,
        int LoyaltyPoints);
}