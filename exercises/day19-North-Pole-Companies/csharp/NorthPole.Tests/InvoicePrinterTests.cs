using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NorthPole.Tests;

public class InvoicePrinterTests
{
    [Fact]
    public Task ExampleInvoice()
    {
        var elfCompanies = LoadElfCompanies();
        var invoice = LoadInvoice();
        var printer = new InvoicePrinter();

        var result = printer.Print(invoice, elfCompanies);

        return Verify(result);
    }

    // TODO: Add ExampleInvoiceWithTaxes() test here

    private Dictionary<string, ElfCompany> LoadElfCompanies()
    {
        var json = File.ReadAllText("Resources/elfCompanies.json");
        var data = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(json);
        var companies = new Dictionary<string, ElfCompany>();

        foreach (var kvp in data)
        {
            companies[kvp.Key] = new ElfCompany(
                kvp.Value["name"].ToString(),
                kvp.Value["type"].ToString(),
                kvp.Value["region"].ToString()
            );
        }
        return companies;
    }

    private Invoice LoadInvoice()
    {
        var json = File.ReadAllText("Resources/order.json");
        var data = JObject.Parse(json);
        var customer = data["customer"].ToString();
        var deliveries = new List<Delivery>();

        foreach (var d in data["deliveries"])
        {
            deliveries.Add(new Delivery(
                d["companyID"].ToString(),
                d["packages"].ToObject<int>()
            ));
        }
        return new Invoice(customer, deliveries);
    }
}