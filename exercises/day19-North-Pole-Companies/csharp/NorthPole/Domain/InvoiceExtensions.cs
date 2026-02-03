namespace NorthPole.Domain;

public static class InvoiceExtensions
{
    extension(Invoice invoice)
    {
        public EnrichedInvoice EnrichWith(Dictionary<string, ElfCompany> companies)
        {
            var enrichedDeliveries = from delivery in invoice.Deliveries
                let company = companies[delivery.CompanyId]
                select new EnrichedDelivery(delivery, company, Tax.NoTax);

            return new EnrichedInvoice(invoice.Customer, enrichedDeliveries.ToList());
        }

        public EnrichedInvoice EnrichWith(Dictionary<string, ElfCompany> companies, Dictionary<string, Tax> taxes)
        {
            var enrichedDeliveries = from delivery in invoice.Deliveries
                let company = companies[delivery.CompanyId]
                let tax = taxes[company.RegionName]
                select new EnrichedDelivery(delivery, company, tax);

            return new EnrichedInvoice(invoice.Customer, enrichedDeliveries.ToList());
        }
    }
}