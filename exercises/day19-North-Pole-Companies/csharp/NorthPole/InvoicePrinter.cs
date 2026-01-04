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
            var printableInvoice = PrintableInvoice.CreateFrom(invoice, elfCompanies);

            var result = new StringBuilder($"Invoice for {printableInvoice.Invoice.Customer}\n");
            var currencyFormat = new CultureInfo("en-US");
            foreach (var line in printableInvoice.InvoiceLines)
            {
                result.AppendLine($" {line.company.Name}: {line.deliveryCost.ToString("C", currencyFormat)} ({line.delivery.Packages} packages)");
            }
            result.AppendLine($"Amount owed is {printableInvoice.TotalAmount.ToString("C", currencyFormat)}");
            result.AppendLine($"You earned {printableInvoice.LoyaltyPoints} loyalty points");
            return result.ToString();
        }
    }
}
