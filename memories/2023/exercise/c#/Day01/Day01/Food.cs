namespace Day01;

public record Food(
    DateOnly ExpirationDate,
    bool ApprovedForConsumption,
    Guid? InspectorId)
{
    public bool IsEdible(DateOnlyProvider now) 
        => IsNotExpired(now) &&
        HasBeenApprovedForConsumption() &&
        HasBeenInspected();

    private bool IsNotExpired(DateOnlyProvider now) 
        => ExpirationDate.IsAfter(now());

    private bool HasBeenApprovedForConsumption() 
        => ApprovedForConsumption;

    private bool HasBeenInspected() 
        => InspectorId != null;
}