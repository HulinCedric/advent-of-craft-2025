using NorthPole.Calculations;
using NorthPole.Calculations.DeliveryCosts;
using NorthPole.Calculations.LoyaltyPoints;

namespace NorthPole.Printers;

public class InvoicePrinter(InvoiceCalculator invoiceCalculator)
{
    public InvoicePrinter() : this(
        new InvoiceCalculator(
            new Dictionary<string, IDeliveryCostCalculator>
            {
                [ElfCompany.ExpressType] = new ExpressDeliveryCostCalculator(),
                [ElfCompany.StandardType] = new StandardDeliveryCostCalculator()
            },
            new Dictionary<string, ILoyaltyPointsCalculator>
            {
                [ElfCompany.ExpressType] = new ExpressLoyaltyPointsCalculator(),
                [ElfCompany.StandardType] = new StandardLoyaltyPointsCalculator()
            },
            new StandardLoyaltyPointsCalculator()))
    {
    }

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