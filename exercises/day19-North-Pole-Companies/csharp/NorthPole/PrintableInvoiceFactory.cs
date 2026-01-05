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
        => CreateFrom(invoice, elfCompanies, new Dictionary<string, TaxRate>());

    public static PrintableInvoice CreateFrom(
        Invoice invoice,
        Dictionary<string, ElfCompany> elfCompanies,
        Dictionary<string, TaxRate> taxRates)
    {
        var lines = CreateLines(invoice.Deliveries, elfCompanies, taxRates).ToList();

        var subTotalAmount = lines.Sum(l => l.NetAmount);
        var taxTotalAmount = lines.Sum(l => l.TaxAmount);
        var totalAmount = lines.Sum(l => l.GrossAmount);
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
        Dictionary<string, TaxRate> taxRates)
        => from delivery in deliveries
            let company = elfCompanies[delivery.CompanyID]
            let taxRate = taxRates.GetValueOrDefault(company.Region, TaxRate.NoTaxRate)
            select Line(delivery, company, taxRate);

    private static Line Line(Delivery delivery, ElfCompany company, TaxRate taxRate)
    {
        var (netAmount, taxAmount, grossAmount) = LineAmounts(delivery, company, taxRate);

        var loyaltyPoints = LineLoyaltyPoints(delivery, company);

        return new Line(
            delivery.Packages,
            company.Name,
            taxRate.Name,
            taxRate.TaxRateValue,
            netAmount,
            taxAmount,
            grossAmount,
            loyaltyPoints);
    }

    private static int LineLoyaltyPoints(Delivery delivery, ElfCompany company)
    {
        var loyaltyPointCalculator = CreateLoyaltyPointCalculator(company.Type);

        return loyaltyPointCalculator.Calculate(delivery.Packages);
    }

    private static (decimal netAmount, decimal taxAmount, decimal grossAmount) LineAmounts(
        Delivery delivery,
        ElfCompany company,
        TaxRate taxRate)
    {
        var deliveryCostCalculator = CreateDeliveryCostCalculator(company.Type);

        var netAmount = deliveryCostCalculator.Calculate(delivery.Packages);
        var taxAmount = CalculateTaxAmount(netAmount, taxRate.TaxRateValue);
        var grossAmount = CalculateGrossAmount(netAmount, taxAmount);

        return (netAmount, taxAmount, grossAmount);
    }

    private static decimal CalculateGrossAmount(decimal netAmount, decimal taxAmount) => netAmount + taxAmount;
    private static decimal CalculateTaxAmount(decimal netAmount, decimal taxRate) => netAmount * taxRate;

    private static IDeliveryCostCalculator CreateDeliveryCostCalculator(string companyType)
        => DeliveryCostCalculators.TryGetValue(companyType, out var calculator)
            ? calculator
            : throw new Exception($"unknown type: {companyType}");

    private static ILoyaltyPointsCalculator CreateLoyaltyPointCalculator(string companyType)
        => LoyaltyPointsCalculators.GetValueOrDefault(companyType, DefaultLoyaltyPointsCalculator);
}