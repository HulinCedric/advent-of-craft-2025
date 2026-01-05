namespace NorthPole;

public class StandardDeliveryCostCalculator : IDeliveryCostCalculator
{
    private const decimal BaseFee = 300m; // 300.00 €
    private const decimal PricePerPackage = 2m; // 2.00 € per package
    private const decimal HighVolumeSurcharge = 10m; // +10.00 € once above 50 packages
    private const decimal ExtraPerPackageAboveThreshold = 3m; // +3.00 € per package above 50

    private const int HighVolumeThreshold = 50;

    public decimal Calculate(int numberOfPackages)
        => numberOfPackages <= HighVolumeThreshold
            ? DeliveryCost(numberOfPackages)
            : DeliveryCostForHighVolume(numberOfPackages);

    private static decimal DeliveryCost(int numberOfPackages)
        => BaseFee
           + numberOfPackages * PricePerPackage;

    private static decimal DeliveryCostForHighVolume(int numberOfPackages)
    {
        var extraPackages = numberOfPackages - HighVolumeThreshold;

        return BaseFee
               + numberOfPackages * PricePerPackage
               + HighVolumeSurcharge
               + extraPackages * ExtraPerPackageAboveThreshold;
    }
}