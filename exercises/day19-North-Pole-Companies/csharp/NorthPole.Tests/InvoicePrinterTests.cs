using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NorthPole.Calculations;
using NorthPole.Calculations.DeliveryCosts;
using NorthPole.Calculations.LoyaltyPoints;
using NorthPole.Printers;

namespace NorthPole.Tests;

public class InvoicePrinterTests
{
    private const string ResourcesOrderWithTaxes = "Resources/orderWithTaxes.json";
    private const string ResourcesOrder = "Resources/order.json";

    private readonly InvoicePrinter _invoicePrinter;

    public InvoicePrinterTests()
        => _invoicePrinter = new InvoicePrinter(
            new InvoiceCalculator(
                new Dictionary<string, IDeliveryCostCalculator>
                {
                    { ElfCompany.ExpressType, new ExpressDeliveryCostCalculator() },
                    { ElfCompany.StandardType, new StandardDeliveryCostCalculator() }
                },
                new Dictionary<string, ILoyaltyPointsCalculator>
                {
                    { ElfCompany.ExpressType, new ExpressLoyaltyPointsCalculator() },
                    { ElfCompany.StandardType, new StandardLoyaltyPointsCalculator() }
                },
                new StandardLoyaltyPointsCalculator()));

    [Fact]
    public Task ExampleInvoice()
    {
        var elfCompanies = LoadElfCompanies();
        var invoice = LoadInvoice(ResourcesOrder);

        var result = _invoicePrinter.PrintWithoutTax(invoice, elfCompanies);

        return Verify(result);
    }

    [Fact]
    public Task ExampleInvoiceWithTaxes()
    {
        var elfCompanies = LoadElfCompanies();
        var invoice = LoadInvoice(ResourcesOrderWithTaxes);
        var taxes = LoadTaxes();

        var result = _invoicePrinter.PrintWithTax(invoice, elfCompanies, taxes);

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

    private static Dictionary<string, Tax> LoadTaxes()
    {
        var json = File.ReadAllText("Resources/taxRates.json");
        var data = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(json);
        var taxes = new Dictionary<string, Tax>();

        foreach (var kvp in data)
        {
            taxes[kvp.Key] = new Tax(
                RegionName: kvp.Value["name"].ToString(),
                Rate: new TaxRate(kvp.Value["taxRate"].ToObject<decimal>()));
        }

        return taxes;
    }
}