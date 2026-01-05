namespace NorthPole;

public interface IDeliveryCostCalculator
{
    decimal Calculate(int numberOfPackages);
}