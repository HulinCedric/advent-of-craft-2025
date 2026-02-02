using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NorthPole.Tests;

public class InvoicePrinterTests
{
    private const string ResourcesOrderWithTaxes = "Resources/orderWithTaxes.json";
    private const string ResourcesOrder = "Resources/order.json";

    [Fact]
    public Task ExampleInvoice()
    {
        var elfCompanies = LoadElfCompanies();
        var invoice = LoadInvoice(ResourcesOrder);

        var result = InvoicePrinter.Print(
            PrintableInvoiceFactory.CreateFrom(invoice, elfCompanies),
            new PrintWithoutTax());

        return Verify(result);
    }

    [Fact]
    public Task ExampleInvoiceWithTaxes()
    {
        var elfCompanies = LoadElfCompanies();
        var invoice = LoadInvoice(ResourcesOrderWithTaxes);
        var taxes = LoadTaxes();

        var result = InvoicePrinter.Print(
            PrintableInvoiceFactory.CreateFrom(invoice, elfCompanies, taxes),
            new PrintWithTax());

        return Verify(result);
    }

    private static Dictionary<string, ElfCompany> LoadElfCompanies()
    {
        var json = File.ReadAllText("Resources/elfCompanies.json");
        var data = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(json);
        var companies = new Dictionary<string, ElfCompany>();

        foreach (var kvp in data)
        {
            companies[kvp.Key] = new ElfCompany(
                kvp.Value["name"].ToString(),
                kvp.Value["type"].ToString(),
                kvp.Value["region"].ToString());
        }

        return companies;
    }

    private static Invoice LoadInvoice(string orders)
    {
        var json = File.ReadAllText(orders);
        var data = JObject.Parse(json);
        var customer = data["customer"].ToString();
        var deliveries = new List<Delivery>();

        foreach (var d in data["deliveries"])
        {
            deliveries.Add(
                new Delivery(
                    d["companyID"].ToString(),
                    d["packages"].ToObject<int>()));
        }

        return new Invoice(customer, deliveries);
    }

    private static Dictionary<string, TaxRate> LoadTaxes()
    {
        var json = File.ReadAllText("Resources/taxRates.json");
        var data = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(json);
        var taxes = new Dictionary<string, TaxRate>();

        foreach (var kvp in data)
        {
            taxes[kvp.Key] = new TaxRate(
                name: kvp.Value["name"].ToString(),
                taxRate: kvp.Value["taxRate"].ToObject<decimal>(),
                description: kvp.Value["description"].ToString());
        }

        return taxes;
    }
}