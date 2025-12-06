namespace Gifts;

public sealed class NaughtyBehaviorFactory : IBehaviorFactory
{
    public IBehavior Create() => new NaughtyBehavior();
}

