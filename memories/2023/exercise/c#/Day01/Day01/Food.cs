namespace Day01;

public record Food(
    DateOnly ExpirationDate,
    bool ApprovedForConsumption,
    Guid? InspectorId)
{
    public bool IsEdible(Func<DateOnly> now)
    {
        if (IsFresh(now) &&
            CanBeConsumed() &&
            HaveBeenInspected())
            return true;
        return false;
    }

    private bool HaveBeenInspected() => InspectorId != null;

    private bool CanBeConsumed() => ApprovedForConsumption;

    private bool IsFresh(Func<DateOnly> now) => ExpirationDate > now();
}