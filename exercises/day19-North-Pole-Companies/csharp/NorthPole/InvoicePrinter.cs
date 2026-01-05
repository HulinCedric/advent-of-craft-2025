namespace NorthPole;

public class InvoicePrinter
{
    public string Print(
        Invoice invoice,
        Dictionary<string, ElfCompany> elfCompanies,
        Dictionary<string, TaxRate> taxes)
    {
        var printableInvoice = PrintableInvoiceFactory.CreateFrom(invoice, elfCompanies, taxes);

        return new PrintWithTax().Print(printableInvoice);
    }

    public string Print(Invoice invoice, Dictionary<string, ElfCompany> elfCompanies)
    {
        var printableInvoice = PrintableInvoiceFactory.CreateFrom(invoice, elfCompanies);

        return new TextPrinter().Print(printableInvoice);
    }
}