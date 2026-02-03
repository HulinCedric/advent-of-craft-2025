namespace NorthPole.Domain;

public record CalculatedTax(Tax Tax, Money Amount)
{
    public override string ToString() => $"Tax ({Tax.RegionName} - {Tax.Rate}): {Amount}";
}