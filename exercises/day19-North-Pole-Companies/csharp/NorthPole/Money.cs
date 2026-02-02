using System.Globalization;

namespace NorthPole;

public readonly record struct Money(decimal Value)
{
    public static readonly Money Zero = new(0m);
    private static readonly CultureInfo Culture = new("en-US");
    
    public static Money operator +(Money a, Money b) => new(a.Value + b.Value);

    public override string ToString() => Value.ToString("C", Culture);
}