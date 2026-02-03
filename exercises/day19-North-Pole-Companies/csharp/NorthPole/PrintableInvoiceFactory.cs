using static NorthPole.PrintableInvoice;

namespace NorthPole;

public class PrintableInvoiceFactory
{
    private readonly ILoyaltyPointsCalculator _defaultLoyaltyPointsCalculator;
    private readonly IReadOnlyDictionary<string, IDeliveryCostCalculator> _deliveryCostCalculators;
    private readonly IReadOnlyDictionary<string, ILoyaltyPointsCalculator> _loyaltyPointsCalculators;

    public PrintableInvoiceFactory(
        IReadOnlyDictionary<string, IDeliveryCostCalculator> deliveryCostCalculators,
        IReadOnlyDictionary<string, ILoyaltyPointsCalculator> loyaltyPointsCalculators,
        ILoyaltyPointsCalculator defaultLoyaltyPointsCalculator)
    {
        _deliveryCostCalculators = deliveryCostCalculators;
        _defaultLoyaltyPointsCalculator = defaultLoyaltyPointsCalculator;
        _loyaltyPointsCalculators = loyaltyPointsCalculators;
    }

    public PrintableInvoice CreateFrom(
        Invoice invoice,
        Dictionary<string, ElfCompany> elfCompanies)
        => CreateFrom(invoice, elfCompanies, new Dictionary<string, Tax>());

    public PrintableInvoice CreateFrom(
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

    private IEnumerable<Line> CreateLines(
        List<Delivery> deliveries,
        Dictionary<string, ElfCompany> elfCompanies,
        Dictionary<string, Tax> taxRates)
        => from delivery in deliveries
            let company = elfCompanies[delivery.CompanyId]
            let taxRate = taxRates.GetValueOrDefault(company.RegionName, Tax.NoTax)
            select Line(delivery, company, taxRate);

    private Line Line(Delivery delivery, ElfCompany company, Tax tax)
    {
        var (netAmount, taxAmount, grossAmount) = LineAmounts(company.Type, delivery.Packages, tax.Rate);

        var loyaltyPoints = LineLoyaltyPoints(company.Type, delivery.Packages);

        return new Line(
            delivery.Packages,
            company.Name,
            new TaxLine(tax, new Money(taxAmount)),
            new Money(netAmount),
            new Money(taxAmount),
            new Money(grossAmount),
            loyaltyPoints);
    }

    private int LineLoyaltyPoints(string companyType, int numberOfPackages)
    {
        var loyaltyPointCalculator = CreateLoyaltyPointCalculator(companyType);

        return loyaltyPointCalculator.Calculate(numberOfPackages);
    }

    private (decimal netAmount, decimal taxAmount, decimal grossAmount) LineAmounts(
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

    private IDeliveryCostCalculator CreateDeliveryCostCalculator(string companyType)
        => _deliveryCostCalculators.TryGetValue(companyType, out var calculator)
            ? calculator
            : throw new Exception($"unknown type: {companyType}");

    private ILoyaltyPointsCalculator CreateLoyaltyPointCalculator(string companyType)
        => _loyaltyPointsCalculators.GetValueOrDefault(companyType, _defaultLoyaltyPointsCalculator);
}