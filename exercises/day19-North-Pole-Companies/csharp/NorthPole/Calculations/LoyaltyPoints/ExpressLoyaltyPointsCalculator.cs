namespace NorthPole.Calculations.LoyaltyPoints;

public class ExpressLoyaltyPointsCalculator : ILoyaltyPointsCalculator
{
    public int Calculate(int numberOfPackages)
    {
        var points = Math.Max(numberOfPackages - 50, 0);
        points += (int)Math.Floor(numberOfPackages / 10.0);
        return points;
    }
}