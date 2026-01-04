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
        switch (company.Type)
        {
            case "express":
                return ExpressCalculateDeliveryCostInCents(delivery) / 100.0m;
            case "standard":
                return StandardCalculateDeliveryCost(delivery.Packages);
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

    private static decimal StandardCalculateDeliveryCost(int numberOfPackages)
    {
        const decimal baseFee = 300m; // 300.00 €
        const decimal pricePerPackage = 2m; // 2.00 € per package
        
        const int threshold = 50;

        if (numberOfPackages <= threshold)
        {
            return baseFee
                   + numberOfPackages * pricePerPackage;
        }

        const decimal highVolumeSurcharge = 10m; // +10.00 € once above 50 packages
        const decimal extraPerPackageAboveThreshold = 3m; // +3.00 € per package above 50
            
        var extraPackages = numberOfPackages - threshold;

        return baseFee
               + numberOfPackages * pricePerPackage
               + highVolumeSurcharge
               + extraPackages * extraPerPackageAboveThreshold;

    }

    private static int CalculateLoyaltyPoints(Delivery delivery, ElfCompany company)
    {
        var points = Math.Max(delivery.Packages - 50, 0);
        if (company.Type == "express") points += (int)Math.Floor(delivery.Packages / 10.0);
        return points;
    }
}