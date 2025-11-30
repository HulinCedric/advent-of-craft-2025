namespace Day01;

using IProvideDate = Func<DateOnly>;

public record Food(
    DateOnly ExpirationDate,
    bool ApprovedForConsumption,
    Guid? InspectorId)
{
    public bool IsEdible(IProvideDate now)
        => IsFresh(now) &&
           CanBeConsumed() &&
           HasBeenInspected();

    private bool IsFresh(IProvideDate now) => ExpirationDate.IsPassed(now());

    private bool CanBeConsumed() => ApprovedForConsumption;

    private bool HasBeenInspected() => InspectorId != null;
}