namespace Gifts;

public interface IBehaviorFactoryResolver
{
    IBehaviorFactory? GetFactory(string? behaviorKey);
    void Register(string behaviorKey, IBehaviorFactory factory);
}

