namespace Day01;

public record Food(
    DateOnly ExpirationDate,
    bool ApprovedForConsumption,
    Guid? InspectorId)
{
    public bool IsEdible(Func<DateOnly> now)
    {
        if (IsFresh(now) &&
            ApprovedForConsumption &&
            InspectorId != null)
            return true;
        return false;
    }

    private bool IsFresh(Func<DateOnly> now) => ExpirationDate > now();
}