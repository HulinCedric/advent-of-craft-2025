using System.Collections.Generic;

namespace NorthPole
{
    public class Invoice
    {
        public string Customer { get; }
        public List<Delivery> Deliveries { get; }

        public Invoice(string customer, List<Delivery> deliveries)
        {
            Customer = customer;
            Deliveries = deliveries;
        }
    }

    public class Delivery
    {
        public string CompanyID { get; }
        public int Packages { get; }

        public Delivery(string companyID, int packages)
        {
            CompanyID = companyID;
            Packages = packages;
        }
    }

    public class ElfCompany
    {
        public string Name { get; }
        public string Type { get; }
        public string Region { get; }

        public ElfCompany(string name, string type, string region)
        {
            Name = name;
            Type = type;
            Region = region;
        }
    }

    public class TaxRate
    {
        public string Name { get; }
        public decimal TaxRateValue { get; }
        public string Description { get; }

        public TaxRate(string name, decimal taxRate, string description)
        {
            Name = name;
            TaxRateValue = taxRate;
            Description = description;
        }
    }
}
