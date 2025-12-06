namespace Gifts;

public sealed class ChildFactory
{
    private readonly IBehaviorFactoryResolver _resolver;

    public ChildFactory(IBehaviorFactoryResolver resolver) => _resolver = resolver;

    public Child Create(string name, string behaviorKey)
    {
        var factory = _resolver.GetFactory(behaviorKey);

        var behavior = factory?.Create();

        return new Child(name, behavior);
    }
}