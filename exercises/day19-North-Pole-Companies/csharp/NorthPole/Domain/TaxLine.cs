namespace NorthPole.Domain;

public record TaxLine(Tax Tax, Money Amount)
{
    public override string ToString() => $"Tax ({Tax.RegionName} - {Tax.Rate}): {Amount}";
}