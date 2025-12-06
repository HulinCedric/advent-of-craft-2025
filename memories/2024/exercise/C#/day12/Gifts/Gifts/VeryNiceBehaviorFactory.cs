namespace Gifts;

public sealed class VeryNiceBehaviorFactory : IBehaviorFactory
{
    public IBehavior Create() => new VeryNiceBehavior();
}