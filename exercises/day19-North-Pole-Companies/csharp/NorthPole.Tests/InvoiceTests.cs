namespace NorthPole.Tests;

public class InvoiceTests
{
    [Theory]
    [InlineData(0, 300.00)]
    [InlineData(1, 302.00)]
    [InlineData(10, 320.00)]
    [InlineData(20, 340.00)]
    [InlineData(48, 396.00)]
    [InlineData(49, 398.00)]
    [InlineData(50, 400.00)]
    [InlineData(51, 415.00)]
    [InlineData(52, 420.00)]
    [InlineData(53, 425.00)]
    [InlineData(80, 560.00)]
    public void Standard_calculation_harness(int packages, decimal deliveryCost)
    {
        var delivery = new Delivery("jingles-standard", packages);
        var company = new ElfCompany("Jingle's Standard Service", "standard", "nordic");

        Assert.Equal(deliveryCost, PrintableInvoice.CalculateDeliveryCost(delivery, company));
    }
    
    [Theory]
    [InlineData(0, 500.00)]
    [InlineData(1, 500.00)]
    [InlineData(2, 500.00)]
    [InlineData(10, 500.00)]
    [InlineData(95, 500.00)]
    [InlineData(99, 500.00)]
    [InlineData(100, 500.00)]
    [InlineData(101, 505.00)]
    [InlineData(102, 510.00)]
    [InlineData(103, 515.00)]
    [InlineData(120, 600.00)]
    public void Express_calculation_harness(int packages, decimal deliveryCost)
    {
        var delivery = new Delivery("rudolph-express", packages);
        var company = new ElfCompany("Rudolph Express Delivery", "express", "north-pole");

        Assert.Equal(deliveryCost, PrintableInvoice.CalculateDeliveryCost(delivery, company));
    }
}