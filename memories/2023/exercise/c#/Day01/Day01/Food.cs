namespace Day01;

public record Food(
    DateOnly ExpirationDate,
    bool ApprovedForConsumption,
    Guid? InspectorId)
{
    public bool IsEdible(Func<DateOnly> now)
        => IsFresh(now) &&
           CanBeConsumed() &&
           HaveBeenInspected();

    private bool IsFresh(Func<DateOnly> now) => ExpirationDate > now();

    private bool CanBeConsumed() => ApprovedForConsumption;

    private bool HaveBeenInspected() => InspectorId != null;
}