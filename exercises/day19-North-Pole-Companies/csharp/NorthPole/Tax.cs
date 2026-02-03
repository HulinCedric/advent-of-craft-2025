namespace NorthPole;

public record Tax(string RegionName, TaxRate Rate)
{
    public static readonly Tax NoTax = new("No Tax", TaxRate.Zero);
}