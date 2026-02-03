using NorthPole.Calculations;

namespace NorthPole.Domain;

public static class InvoiceExtensions
{
    extension(Invoice invoice)
    {
        public CalculatedInvoice CalculateWithoutTaxes(
            InvoiceCalculator calculator,
            Dictionary<string, ElfCompany> elfCompanies)
            => calculator.Calculate(invoice, elfCompanies);

        public CalculatedInvoice CalculateWithTaxes(
            InvoiceCalculator calculator,
            Dictionary<string, ElfCompany> elfCompanies,
            Dictionary<string, Tax> taxes)
            => calculator.Calculate(invoice, elfCompanies, taxes);
    }

    extension(Invoice invoice)
    {
        public EnrichedInvoice EnrichWith(Dictionary<string, ElfCompany> companies)
            => new(
                invoice.Customer,
                invoice.Deliveries
                    .Select(d => new EnrichedDelivery(d, new EnrichedElfCompany(companies[d.CompanyId], Tax.NoTax)))
                    .ToList());

        public EnrichedInvoice EnrichWith(Dictionary<string, ElfCompany> companies, Dictionary<string, Tax> taxes)
            => new(
                invoice.Customer,
                invoice.Deliveries
                    .Select(d => new EnrichedDelivery(
                        d,
                        new EnrichedElfCompany(
                            companies[d.CompanyId],
                            taxes[companies[d.CompanyId].RegionName])))
                    .ToList());
    }
}