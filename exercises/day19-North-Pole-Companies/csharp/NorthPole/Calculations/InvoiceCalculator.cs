using NorthPole.Calculations.DeliveryCosts;
using NorthPole.Calculations.LoyaltyPoints;
using static NorthPole.CalculatedInvoice;

namespace NorthPole.Calculations;

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

        return Create(invoice, lines);
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
        var taxLine = TaxLine(tax, netAmount);
        var loyaltyPoints = LoyaltyPoints(delivery, company);

        return new Line(
            delivery.NumberOfPackages,
            company.Name,
            taxLine,
            netAmount,
            loyaltyPoints);
    }

    private Money NetAmount(Delivery delivery, ElfCompany company)
        => _deliveryCostCalculators.TryGetValue(company.Type, out var calculator)
            ? new Money(calculator.Calculate(delivery.NumberOfPackages))
            : throw new InvalidOperationException($"Unknown company type: {company.Type}");

    private static TaxLine TaxLine(Tax tax, Money netAmount) => new(tax, new Money(netAmount.Value * tax.Rate.Value));

    private int LoyaltyPoints(Delivery delivery, ElfCompany company)
        => _loyaltyPointsCalculators
            .GetValueOrDefault(company.Type, _defaultLoyaltyPointsCalculator)
            .Calculate(delivery.NumberOfPackages);
}