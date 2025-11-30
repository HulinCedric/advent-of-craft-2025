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
           HaveBeenInspected();

    private bool IsFresh(IProvideDate now) => ExpirationDate > now();

    private bool CanBeConsumed() => ApprovedForConsumption;

    private bool HaveBeenInspected() => InspectorId != null;
}