namespace NorthPole;

public class Tax
{
    public string Name { get; }
    public TaxRate TaxRate { get; }
    public string Description { get; }

    public Tax(string name, TaxRate taxRate, string description)
    {
        Name = name;
        TaxRate = taxRate;
        Description = description;
    }

    public static readonly Tax NoTax = new("No Tax", TaxRate.Zero, "No tax rate for this region");
}