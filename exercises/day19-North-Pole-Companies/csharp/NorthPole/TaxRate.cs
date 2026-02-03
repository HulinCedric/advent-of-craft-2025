using System.Globalization;

namespace NorthPole;

public readonly record struct TaxRate(decimal Value)
{
    private static readonly CultureInfo Culture = new("en-US");

    public static readonly TaxRate Zero = new(0m);

    public static implicit operator decimal(TaxRate taxRate) => taxRate.Value;

    public override string ToString() => Value.ToString("P0", Culture);
}