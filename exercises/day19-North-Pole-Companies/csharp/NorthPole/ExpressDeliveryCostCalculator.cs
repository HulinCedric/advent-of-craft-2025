namespace NorthPole;

public class ExpressDeliveryCostCalculator : IDeliveryCostCalculator
{
    private const decimal BaseFee = 500m; // 500.00 €
    private const decimal ExtraPerPackageAboveThreshold = 5m; // +5.00 € per package above 100

    private const int HighVolumeThreshold = 100;

    public decimal Calculate(int numberOfPackages)
        => numberOfPackages <= HighVolumeThreshold
            ? DeliveryCost()
            : DeliveryCostForHighVolume(numberOfPackages);

    private static decimal DeliveryCost() => BaseFee;

    private static decimal DeliveryCostForHighVolume(int numberOfPackages)
    {
        var extraPackages = numberOfPackages - HighVolumeThreshold;

        return BaseFee
               + extraPackages * ExtraPerPackageAboveThreshold;
    }
}