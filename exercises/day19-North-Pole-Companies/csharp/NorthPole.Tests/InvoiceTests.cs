namespace NorthPole.Tests;

public class InvoiceTests
{
    [Fact]
    public void Standard_calculation_harness()
    {
        var delivery = new Delivery("jingles-standard", 80);
        var company = new ElfCompany("Jingle's Standard Service", "standard", "nordic");

        var deliveryCost = PrintableInvoice.CalculateDeliveryCost(delivery, company);
        Assert.Equal(560.00m, deliveryCost);
    }
}