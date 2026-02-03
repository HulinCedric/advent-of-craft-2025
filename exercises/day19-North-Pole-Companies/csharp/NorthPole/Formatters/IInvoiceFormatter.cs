using NorthPole.Domain;

namespace NorthPole.Formatters;

public interface IInvoiceFormatter
{
    string Format(CalculatedInvoice invoice);
}