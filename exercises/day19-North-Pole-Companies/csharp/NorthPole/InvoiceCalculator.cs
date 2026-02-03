using static NorthPole.CalculatedInvoice;

namespace NorthPole;

public class InvoiceCalculator
{
    private readonly ILoyaltyPointsCalculator _defaultLoyaltyPointsCalculator;
    private readonly IReadOnlyDictionary<string, IDeliveryCostCalculator> _deliveryCostCalculators;
    private readonly IReadOnlyDictionary<string, ILoyaltyPointsCalculator> _loyaltyPointsCalculators;

    public InvoiceCalculator(
        IReadOnlyDictionary<string, IDeliveryCostCalculator> deliveryCostCalculators,
        IReadOnlyDictionary<string, ILoyaltyPointsCalculator> loyaltyPointsCalculators,
        ILoyaltyPointsCalculator defaultLoyaltyPointsCalculator)
    {
        _deliveryCostCalculators = deliveryCostCalculators;
        _defaultLoyaltyPointsCalculator = defaultLoyaltyPointsCalculator;
        _loyaltyPointsCalculators = loyaltyPointsCalculators;
    }

    public CalculatedInvoice CreateFrom(
        Invoice invoice,
        Dictionary<string, ElfCompany> elfCompanies)
        => CreateFrom(invoice, elfCompanies, new Dictionary<string, Tax>());

    public CalculatedInvoice CreateFrom(
        Invoice invoice,
        Dictionary<string, ElfCompany> elfCompanies,
        Dictionary<string, Tax> taxRates)
    {
        var lines = CreateLines(invoice.Deliveries, elfCompanies, taxRates).ToList();

        var subTotalAmount = lines.Sum(line => line.NetAmount);
        var taxTotalAmount = lines.Sum(line => line.TaxAmount);
        var totalAmount = lines.Sum(line => line.GrossAmount);
        var loyaltyPoints = lines.Sum(l => l.LoyaltyPoints);

        return new CalculatedInvoice(
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
        var netAmount = NetAmount(delivery, company);
        var taxAmount = TaxAmount(netAmount, tax.Rate);
        var taxLine = new TaxLine(tax, taxAmount);
        var grossAmount = GrossAmount(netAmount, taxAmount);
        var loyaltyPoints = LoyaltyPoints(delivery, company);

        return new Line(
            delivery.NumberOfPackages,
            company.Name,
            taxLine,
            netAmount,
            taxAmount,
            grossAmount,
            loyaltyPoints);
    }

    private Money NetAmount(Delivery delivery, ElfCompany company)
    {
        var deliveryCostCalculator = CreateDeliveryCostCalculator(company.Type);

        return new Money(deliveryCostCalculator.Calculate(delivery.NumberOfPackages));
    }

    private static Money GrossAmount(Money netAmount, Money taxAmount) => new(netAmount.Value + taxAmount.Value);

    private static Money TaxAmount(Money netAmount, TaxRate taxRate) => new(netAmount.Value * taxRate.Value);

    private int LoyaltyPoints(Delivery delivery, ElfCompany company)
    {
        var loyaltyPointCalculator = CreateLoyaltyPointCalculator(company.Type);

        return loyaltyPointCalculator.Calculate(delivery.NumberOfPackages);
    }

    private IDeliveryCostCalculator CreateDeliveryCostCalculator(string companyType)
        => _deliveryCostCalculators.TryGetValue(companyType, out var calculator)
            ? calculator
            : throw new Exception($"unknown type: {companyType}");

    private ILoyaltyPointsCalculator CreateLoyaltyPointCalculator(string companyType)
        => _loyaltyPointsCalculators.GetValueOrDefault(companyType, _defaultLoyaltyPointsCalculator);
}