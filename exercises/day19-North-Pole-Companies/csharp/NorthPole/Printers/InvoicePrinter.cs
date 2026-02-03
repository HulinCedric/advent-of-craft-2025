namespace NorthPole;

public static class InvoicePrinter
{
    public static string Print(
        PrintableInvoice invoice,
        IPrinter printer)
        => printer.Print(invoice);
}