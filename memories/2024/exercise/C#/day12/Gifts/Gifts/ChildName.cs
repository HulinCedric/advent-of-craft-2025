namespace Gifts;

public sealed record ChildName(string Name) : IComparable<ChildName>
{
    public int CompareTo(ChildName? other)
    {
        if (other is null) return 1;
        return string.CompareOrdinal(Name, other.Name);
    }

    public static implicit operator ChildName(string name) => new(name);
}