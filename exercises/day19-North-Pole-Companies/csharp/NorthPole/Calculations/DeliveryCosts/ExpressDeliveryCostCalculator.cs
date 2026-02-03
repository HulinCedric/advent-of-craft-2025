using NorthPole.Domain;

namespace NorthPole.Calculations.DeliveryCosts;

public class ExpressDeliveryCostCalculator : IDeliveryCostCalculator
{
    private const int HighVolumeThreshold = 100;
    private static readonly Money BaseCost = Money.Parse(500.00m);
    private static readonly Money ExtraPackageCost = Money.Parse(5.00m);

    public Money Calculate(int numberOfPackages)
        => numberOfPackages <= HighVolumeThreshold
            ? DeliveryCost()
            : DeliveryCostForHighVolume(numberOfPackages);

    private static Money DeliveryCost() => BaseCost;

    private static Money DeliveryCostForHighVolume(int numberOfPackages)
    {
        var extraPackages = numberOfPackages - HighVolumeThreshold;

        return BaseCost + extraPackages * ExtraPackageCost;
    }
}