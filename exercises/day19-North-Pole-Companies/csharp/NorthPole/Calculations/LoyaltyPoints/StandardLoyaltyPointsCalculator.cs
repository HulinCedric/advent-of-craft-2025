namespace NorthPole.Calculations.LoyaltyPoints;

public class StandardLoyaltyPointsCalculator : ILoyaltyPointsCalculator
{
    public int Calculate(int numberOfPackages) => Math.Max(numberOfPackages - 50, 0);
}