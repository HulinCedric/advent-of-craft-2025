using NorthPole.Domain;

namespace NorthPole.Calculations;

public record CalculatedTax(Tax Tax, Money Amount)
{
    public override string ToString() => $"Tax ({Tax.RegionName} - {Tax.Rate}): {Amount}";
}