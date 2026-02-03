namespace NorthPole;

public class Tax
{
    public string Name { get; }
    public decimal TaxRateValue { get; }
    public string Description { get; }

    public Tax(string name, decimal taxRate, string description)
    {
        Name = name;
        TaxRateValue = taxRate;
        Description = description;
    }

    public static readonly Tax NoTax = new("No Tax", 0m, "No tax rate for this region");
}