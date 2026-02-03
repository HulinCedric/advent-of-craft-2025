using NorthPole.Calculations;
using NorthPole.Calculations.DeliveryCosts;
using NorthPole.Calculations.LoyaltyPoints;
using NorthPole.Domain;

namespace NorthPole.Formatters;

public class InvoicePrinter(InvoiceCalculator calculator)
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
        => invoice.EnrichWith(elfCompanies)
            .CalculateWithoutTaxes(calculator, elfCompanies)
            .FormatWith(new InvoiceFormatterWithoutTax());

    public string PrintWithTax(
        Invoice invoice,
        Dictionary<string, ElfCompany> elfCompanies,
        Dictionary<string, Tax> taxes)
        => invoice
            .EnrichWith(elfCompanies, taxes)
            .CalculateWithTaxes(calculator, elfCompanies, taxes)
            .FormatWith(new InvoiceFormatterWithTax());
}