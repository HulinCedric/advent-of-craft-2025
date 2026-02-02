using System.Globalization;

namespace NorthPole;

public readonly record struct Money(decimal Value)
{
    private readonly CultureInfo _culture = new("en-US");

    public override string ToString() => Value.ToString("C", _culture);

    public static implicit operator decimal(Money money) => money.Value;
}