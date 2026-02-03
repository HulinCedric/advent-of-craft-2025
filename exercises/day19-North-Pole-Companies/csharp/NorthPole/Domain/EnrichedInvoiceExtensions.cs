using NorthPole.Calculations;

namespace NorthPole.Domain;

public static class EnrichedInvoiceExtensions
{
    extension(EnrichedInvoice invoice)
    {
        public CalculatedInvoice CalculateWith(InvoiceCalculator calculator) => calculator.Calculate(invoice);
    }
}