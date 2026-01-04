namespace NorthPole;

public record PrintableInvoice(Invoice Invoice, IReadOnlyList<(Delivery delivery, ElfCompany company, double deliveryCost)> InvoiceLines, double TotalAmount, int LoyaltyPoints)
{
    public static PrintableInvoice CreateFrom(Invoice invoice, Dictionary<string, ElfCompany> elfCompanies)
    {
        var invoiceLines = new List<(Delivery delivery, ElfCompany company, double deliveryCost)>();
        foreach (var delivery in invoice.Deliveries)
        {
            var company = elfCompanies[delivery.CompanyID];
            var deliveryCostInCents = CalculateDeliveryCost(delivery, company);

            invoiceLines.Add((delivery, company, deliveryCostInCents / 100.0));
        }
        
        var totalAmount = invoiceLines.Select(t=>t.deliveryCost).Sum();
            
        var loyaltyPoints = 0;
        foreach (var delivery in invoice.Deliveries)
        {
            var company = elfCompanies[delivery.CompanyID];
            loyaltyPoints += CalculateLoyaltyPoints(delivery, company);
        }
        var printableInvoice = new PrintableInvoice(invoice, invoiceLines, totalAmount, loyaltyPoints);
        return printableInvoice;
    }

    private static int CalculateDeliveryCost(Delivery delivery, ElfCompany company)
    {
        var cost = 0;
        switch (company.Type)
        {
            case "express":
                cost = 50000;
                if (delivery.Packages > 100)
                {
                    cost += 500 * (delivery.Packages - 100);
                }
                break;
            case "standard":
                cost = 30000;
                if (delivery.Packages > 50)
                {
                    cost += 1000 + 300 * (delivery.Packages - 50);
                }
                cost += 200 * delivery.Packages;
                break;
            default:
                throw new Exception($"unknown type: {company.Type}");
        }
        return cost;
    }

    private static int CalculateLoyaltyPoints(Delivery delivery, ElfCompany company)
    {
        var points = Math.Max(delivery.Packages - 50, 0);
        if (company.Type == "express")
        {
            points += (int)Math.Floor(delivery.Packages / 10.0);
        }
        return points;
    }
}