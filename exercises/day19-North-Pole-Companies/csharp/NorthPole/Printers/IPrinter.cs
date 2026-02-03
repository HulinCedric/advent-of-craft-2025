namespace NorthPole.Printers;

public interface IPrinter
{
    string Print(CalculatedInvoice invoice);
}