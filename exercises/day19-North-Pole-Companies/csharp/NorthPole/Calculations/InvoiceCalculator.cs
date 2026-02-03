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
    {
        var lines = invoice.Deliveries.Select(Line).ToList();

        return CalculatedInvoice.From(invoice, lines);
    }

    private CalculatedInvoice.Line Line(EnrichedDelivery delivery)
    {
        var netAmount = NetAmount(delivery);
        var taxLine = TaxLine(delivery, netAmount);
        var loyaltyPoints = LoyaltyPoints(delivery);

        return new CalculatedInvoice.Line(
            delivery.Packages,
            delivery.Company.Name,
            taxLine,
            netAmount,
            loyaltyPoints);
    }

    private Money NetAmount(EnrichedDelivery delivery)
        => deliveryCostCalculators.TryGetValue(delivery.Company.Type, out var calculator)
            ? new Money(calculator.Calculate(delivery.Packages))
            : throw new InvalidOperationException($"Unknown company type: {delivery.Company.Type}");

    private static TaxLine TaxLine(EnrichedDelivery delivery, Money netAmount)
        => new(delivery.Company.Tax, new Money(netAmount.Value * delivery.Company.Tax.Rate.Value));

    private int LoyaltyPoints(EnrichedDelivery delivery)
        => loyaltyPointsCalculators
            .GetValueOrDefault(delivery.Company.Type, defaultLoyaltyPointsCalculator)
            .Calculate(delivery.Packages);
}