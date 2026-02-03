using static NorthPole.PrintableInvoice;

namespace NorthPole;

public static class PrintableInvoiceFactory
{
    private static readonly IReadOnlyDictionary<string, IDeliveryCostCalculator> DeliveryCostCalculators =
        new Dictionary<string, IDeliveryCostCalculator>
        {
            { ElfCompany.ExpressType, new ExpressDeliveryCostCalculator() },
            { ElfCompany.StandardType, new StandardDeliveryCostCalculator() }
        };

    private static readonly ILoyaltyPointsCalculator StandardLoyaltyPointsCalculator =
        new StandardLoyaltyPointsCalculator();

    private static readonly ILoyaltyPointsCalculator DefaultLoyaltyPointsCalculator = StandardLoyaltyPointsCalculator;

    private static readonly IReadOnlyDictionary<string, ILoyaltyPointsCalculator> LoyaltyPointsCalculators =
        new Dictionary<string, ILoyaltyPointsCalculator>
        {
            { ElfCompany.ExpressType, new ExpressLoyaltyPointsCalculator() },
            { ElfCompany.StandardType, StandardLoyaltyPointsCalculator }
        };

    public static PrintableInvoice CreateFrom(
        Invoice invoice,
        Dictionary<string, ElfCompany> elfCompanies)
        => CreateFrom(invoice, elfCompanies, new Dictionary<string, Tax>());

    public static PrintableInvoice CreateFrom(
        Invoice invoice,
        Dictionary<string, ElfCompany> elfCompanies,
        Dictionary<string, Tax> taxRates)
    {
        var lines = CreateLines(invoice.Deliveries, elfCompanies, taxRates).ToList();

        var subTotalAmount = lines.Sum(line => line.NetAmount);
        var taxTotalAmount = lines.Sum(line => line.TaxAmount);
        var totalAmount = lines.Sum(line => line.GrossAmount);
        var loyaltyPoints = lines.Sum(l => l.LoyaltyPoints);

        return new PrintableInvoice(
            invoice.Customer,
            lines,
            subTotalAmount,
            taxTotalAmount,
            totalAmount,
            loyaltyPoints);
    }

    private static IEnumerable<Line> CreateLines(
        List<Delivery> deliveries,
        Dictionary<string, ElfCompany> elfCompanies,
        Dictionary<string, Tax> taxRates)
        => from delivery in deliveries
            let company = elfCompanies[delivery.CompanyID]
            let taxRate = taxRates.GetValueOrDefault(company.Region, Tax.NoTax)
            select Line(delivery, company, taxRate);

    private static Line Line(Delivery delivery, ElfCompany company, Tax tax)
    {
        var (netAmount, taxAmount, grossAmount) = LineAmounts(company.Type, delivery.Packages, tax.TaxRate);

        var loyaltyPoints = LineLoyaltyPoints(company.Type, delivery.Packages);

        return new Line(
            delivery.Packages,
            company.Name,
            tax.Name,
            tax.TaxRate,
            new Money(netAmount),
            new Money(taxAmount),
            new Money(grossAmount),
            loyaltyPoints);
    }

    private static int LineLoyaltyPoints(string companyType, int numberOfPackages)
    {
        var loyaltyPointCalculator = CreateLoyaltyPointCalculator(companyType);

        return loyaltyPointCalculator.Calculate(numberOfPackages);
    }

    private static (decimal netAmount, decimal taxAmount, decimal grossAmount) LineAmounts(
        string companyType,
        int numberOfPackages,
        TaxRate taxRate)
    {
        var deliveryCostCalculator = CreateDeliveryCostCalculator(companyType);

        var netAmount = deliveryCostCalculator.Calculate(numberOfPackages);
        var taxAmount = CalculateTaxAmount(netAmount, taxRate);
        var grossAmount = CalculateGrossAmount(netAmount, taxAmount);

        return (netAmount, taxAmount, grossAmount);
    }

    private static decimal CalculateGrossAmount(decimal netAmount, decimal taxAmount) => netAmount + taxAmount;
    private static decimal CalculateTaxAmount(decimal netAmount, TaxRate taxRate) => netAmount * taxRate;

    private static IDeliveryCostCalculator CreateDeliveryCostCalculator(string companyType)
        => DeliveryCostCalculators.TryGetValue(companyType, out var calculator)
            ? calculator
            : throw new Exception($"unknown type: {companyType}");

    private static ILoyaltyPointsCalculator CreateLoyaltyPointCalculator(string companyType)
        => LoyaltyPointsCalculators.GetValueOrDefault(companyType, DefaultLoyaltyPointsCalculator);
}