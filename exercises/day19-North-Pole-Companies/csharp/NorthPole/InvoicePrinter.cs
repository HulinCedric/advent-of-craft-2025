using NorthPole.Calculations;
using NorthPole.Calculations.DeliveryCosts;
using NorthPole.Calculations.LoyaltyPoints;
using NorthPole.Domain;
using NorthPole.Formatters;

namespace NorthPole;

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

    public string PrintWithoutTax(Invoice invoice, Dictionary<string, ElfCompany> companies)
        => invoice
            .EnrichWith(companies)
            .CalculateWith(calculator)
            .FormatWith(new InvoiceFormatterWithoutTax());

    public string PrintWithTax(
        Invoice invoice,
        Dictionary<string, ElfCompany> companies,
        Dictionary<string, Tax> taxes)
        => invoice
            .EnrichWith(companies, taxes)
            .CalculateWith(calculator)
            .FormatWith(new InvoiceFormatterWithTax());
}