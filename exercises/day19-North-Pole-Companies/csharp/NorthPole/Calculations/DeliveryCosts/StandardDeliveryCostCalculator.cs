using NorthPole.Domain;

namespace NorthPole.Calculations.DeliveryCosts;

public class StandardDeliveryCostCalculator : IDeliveryCostCalculator
{
    private const int HighVolumeThreshold = 50;

    private static readonly Money BaseCost = Money.Parse(300.00m);
    private static readonly Money PerPackageCost = Money.Parse(2.00m);
    private static readonly Money HighVolumeSurcharge = Money.Parse(10.00m);
    private static readonly Money ExtraPackageCost = Money.Parse(3.00m);

    public Money Calculate(int numberOfPackages)
        => numberOfPackages <= HighVolumeThreshold
            ? DeliveryCost(numberOfPackages)
            : DeliveryCostForHighVolume(numberOfPackages);

    private static Money DeliveryCost(int numberOfPackages)
        => BaseCost
           + numberOfPackages * PerPackageCost;

    private static Money DeliveryCostForHighVolume(int numberOfPackages)
    {
        var extraPackages = numberOfPackages - HighVolumeThreshold;

        return BaseCost
               + numberOfPackages * PerPackageCost
               + HighVolumeSurcharge
               + extraPackages * ExtraPackageCost;
    }
}