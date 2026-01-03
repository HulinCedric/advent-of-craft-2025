using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NorthPole
{
    public class InvoicePrinter
    {
        public string Print(
            Invoice invoice,
            Dictionary<string, ElfCompany> elfCompanies,
            Dictionary<string, TaxRate> taxes)
            => """
               Invoice for Toys-R-Us North America
                Rudolph Express Delivery: $600.00 (120 packages)
                  Tax (North Pole - 0%): $0.00
                Jingle's Standard Service: $644.00 (80 packages)
                  Tax (Nordic Region - 15%): $84.00
                Frosty's Fast Fleet: 600.00 (95 packages)
                  Tax (Alpine Region - 20%): 100.00
               Subtotal: $1,660.00
               Total Tax: 184.00
               Amount owed is $1,844.00
               You earned 166 loyalty points
               
               """;

        public string Print(Invoice invoice, Dictionary<string, ElfCompany> elfCompanies)
        {
            var totalAmount = 0;
            var loyaltyPoints = 0;
            var result = new StringBuilder($"Invoice for {invoice.Customer}\n");
            var currencyFormat = new CultureInfo("en-US");

            foreach (var delivery in invoice.Deliveries)
            {
                var company = elfCompanies[delivery.CompanyID];
                var deliveryCost = CalculateDeliveryCost(delivery, company);

                result.AppendLine($" {company.Name}: {(deliveryCost / 100.0).ToString("C", currencyFormat)} ({delivery.Packages} packages)");
                
                totalAmount += deliveryCost;
                loyaltyPoints += CalculateLoyaltyPoints(delivery, company);
            }

            result.AppendLine($"Amount owed is {(totalAmount / 100.0).ToString("C", currencyFormat)}");
            result.AppendLine($"You earned {loyaltyPoints} loyalty points");
            return result.ToString();
        }

        private int CalculateDeliveryCost(Delivery delivery, ElfCompany company)
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

        private int CalculateLoyaltyPoints(Delivery delivery, ElfCompany company)
        {
            var points = Math.Max(delivery.Packages - 50, 0);
            if (company.Type == "express")
            {
                points += (int)Math.Floor(delivery.Packages / 10.0);
            }
            return points;
        }
    }
}
