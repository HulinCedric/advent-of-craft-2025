using NorthPole.Calculations;

namespace NorthPole.Printers;

public class InvoicePrinter(InvoiceCalculator invoiceCalculator)
{
    public string PrintWithoutTax(Invoice invoice, Dictionary<string, ElfCompany> elfCompanies)
    {
        var printableInvoice = invoiceCalculator.CreateFrom(invoice, elfCompanies);
        return new PrintWithoutTax().Print(printableInvoice);
    }

    public string PrintWithTax(
        Invoice invoice,
        Dictionary<string, ElfCompany> elfCompanies,
        Dictionary<string, Tax> taxes)
    {
        var printableInvoice = invoiceCalculator.CreateFrom(invoice, elfCompanies, taxes);
        return new PrintWithTax().Print(printableInvoice);
    }
}