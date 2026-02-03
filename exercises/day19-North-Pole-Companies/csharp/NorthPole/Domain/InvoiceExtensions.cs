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
}