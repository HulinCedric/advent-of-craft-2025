namespace NorthPole;

public class TaxRate
{
    public string Name { get; }
    public decimal TaxRateValue { get; }
    public string Description { get; }

    public TaxRate(string name, decimal taxRate, string description)
    {
        Name = name;
        TaxRateValue = taxRate;
        Description = description;
    }

    public static readonly TaxRate NoTaxRate = new("No Tax", 0m, "No tax rate for this region");
}