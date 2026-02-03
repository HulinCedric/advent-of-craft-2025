using NorthPole.Calculations.DeliveryCosts;
using NorthPole.Calculations.LoyaltyPoints;
using NorthPole.Domain;

namespace NorthPole.Calculations;

public sealed class InvoiceCalculator(
    IReadOnlyDictionary<string, IDeliveryCostCalculator> deliveryCostCalculators,
    IReadOnlyDictionary<string, ILoyaltyPointsCalculator> loyaltyPointsCalculators,
    ILoyaltyPointsCalculator defaultLoyaltyPointsCalculator)
{
    public CalculatedInvoice Calculate(EnrichedInvoice invoice)
        => CalculatedInvoice.From(
            invoice,
            lines: invoice.Deliveries.Select(Line).ToList());

    private CalculatedInvoice.Line Line(EnrichedDelivery delivery)
    {
        var netAmount = NetAmount(delivery);
        var tax = Tax(delivery, netAmount);
        var loyaltyPoints = LoyaltyPoints(delivery);

        return new CalculatedInvoice.Line(
            delivery.Packages,
            delivery.CompanyName,
            tax,
            netAmount,
            loyaltyPoints);
    }

    private Money NetAmount(EnrichedDelivery delivery)
        => deliveryCostCalculators.TryGetValue(delivery.CompanyType, out var calculator)
            ? calculator.Calculate(delivery.Packages)
            : throw new InvalidOperationException($"Unknown company type: {delivery.CompanyType}");

    private static CalculatedTax Tax(EnrichedDelivery delivery, Money netAmount)
        => new(delivery.Tax, Amount: netAmount * delivery.TaxRate);

    private int LoyaltyPoints(EnrichedDelivery delivery)
        => loyaltyPointsCalculators
            .GetValueOrDefault(delivery.CompanyType, defaultLoyaltyPointsCalculator)
            .Calculate(delivery.Packages);
}