namespace NorthPole;

public class InvoicePrinter(PrintableInvoiceFactory printableInvoiceFactory)
{
    public string PrintWithoutTax(Invoice invoice, Dictionary<string, ElfCompany> elfCompanies)
    {
        var printableInvoice = printableInvoiceFactory.CreateFrom(invoice, elfCompanies);
        return new PrintWithoutTax().Print(printableInvoice);
    }

    public string PrintWithTax(
        Invoice invoice,
        Dictionary<string, ElfCompany> elfCompanies,
        Dictionary<string, Tax> taxes)
    {
        var printableInvoice = printableInvoiceFactory.CreateFrom(invoice, elfCompanies, taxes);
        return new PrintWithTax().Print(printableInvoice);
    }
}