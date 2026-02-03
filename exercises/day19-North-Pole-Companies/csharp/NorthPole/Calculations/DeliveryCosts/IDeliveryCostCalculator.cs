namespace NorthPole.Calculations.DeliveryCosts;

public interface IDeliveryCostCalculator
{
    decimal Calculate(int numberOfPackages);
}