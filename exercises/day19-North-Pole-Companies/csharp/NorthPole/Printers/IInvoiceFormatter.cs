namespace NorthPole.Printers;

public interface IInvoiceFormatter
{
    string Format(CalculatedInvoice invoice);
}