using System.Globalization;

namespace NorthPole.Domain;

public readonly record struct Money(decimal Value)
{
    public static readonly Money Zero = new(0m);
    private static readonly CultureInfo Culture = new("en-US");

    public static Money operator +(Money a, Money b) => new(a.Value + b.Value);
    public static Money operator *(Money a, TaxRate b) => new(a.Value * b.Value);
    public static Money operator *(int time, Money money) => new(time * money.Value);

    public override string ToString() => Value.ToString("C", Culture);

    public static Money Parse(decimal amount) => new(amount);
}