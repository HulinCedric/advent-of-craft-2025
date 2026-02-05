using NorthPole.Domain;

namespace NorthPole.Calculations.DeliveryCosts;

public interface IDeliveryCostCalculator
{
    Money Calculate(int numberOfPackages);
}