namespace Gifts;

public sealed class NiceBehaviorFactory : IBehaviorFactory
{
    public IBehavior Create() => new NiceBehavior();
}

