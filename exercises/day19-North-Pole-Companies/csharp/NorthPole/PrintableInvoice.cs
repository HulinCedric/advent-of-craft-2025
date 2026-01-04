namespace NorthPole;

public record PrintableInvoice(
    Invoice Invoice,
    IReadOnlyList<(Delivery delivery, ElfCompany company, decimal deliveryCost)> InvoiceLines,
    decimal TotalAmount,
    int LoyaltyPoints)
{
    public static PrintableInvoice CreateFrom(Invoice invoice, Dictionary<string, ElfCompany> elfCompanies)
    {
        var invoiceLines = new List<(Delivery delivery, ElfCompany company, decimal deliveryCost)>();
        foreach (var delivery in invoice.Deliveries)
        {
            var company = elfCompanies[delivery.CompanyID];
            var deliveryCost = CalculateDeliveryCost(delivery, company);
            invoiceLines.Add((delivery, company, deliveryCost));
        }

        var totalAmount = invoiceLines.Select(t => t.deliveryCost).Sum();

        var loyaltyPoints = 0;
        foreach (var delivery in invoice.Deliveries)
        {
            var company = elfCompanies[delivery.CompanyID];
            loyaltyPoints += CalculateLoyaltyPoints(delivery, company);
        }

        var printableInvoice = new PrintableInvoice(invoice, invoiceLines, totalAmount, loyaltyPoints);
        return printableInvoice;
    }

    public static decimal CalculateDeliveryCost(Delivery delivery, ElfCompany company)
    {
        var deliveryCostInCents = CalculateDeliveryCostInCents(delivery, company);

        return deliveryCostInCents / 100.0m;
    }

    private static int CalculateDeliveryCostInCents(Delivery delivery, ElfCompany company)
    {
        switch (company.Type)
        {
            case "express":
                return ExpressCalculateDeliveryCostInCents(delivery);
            case "standard":
                return StandardCalculateDeliveryCostInCents(delivery);
            default:
                throw new Exception($"unknown type: {company.Type}");
        }
    }

    private static int ExpressCalculateDeliveryCostInCents(Delivery delivery)
    {
        var basePrice = 50000;
        if (delivery.Packages > 100) basePrice += 500 * (delivery.Packages - 100);

        return basePrice;
    }

    private static int StandardCalculateDeliveryCostInCents(Delivery delivery)
    {
        var basePrice = 30000;
        if (delivery.Packages > 50) basePrice += 1000 + 300 * (delivery.Packages - 50);
        basePrice += 200 * delivery.Packages;
        return basePrice;
    }

    private static int CalculateLoyaltyPoints(Delivery delivery, ElfCompany company)
    {
        var points = Math.Max(delivery.Packages - 50, 0);
        if (company.Type == "express") points += (int)Math.Floor(delivery.Packages / 10.0);
        return points;
    }
}