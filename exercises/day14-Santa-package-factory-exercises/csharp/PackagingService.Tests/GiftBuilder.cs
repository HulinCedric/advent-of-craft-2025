namespace PackagingService.Tests;

public class GiftBuilder
{
    private bool _isFragile;
    private GiftSize _size = GiftSize.SMALL;

    public static GiftBuilder AGift() => new();

    public GiftBuilder Small()
    {
        _size = GiftSize.SMALL;
        return this;
    }

    public GiftBuilder ExtraLarge()
    {
        _size = GiftSize.EXTRA_LARGE;
        return this;
    }

    public GiftBuilder NonFragile()
    {
        _isFragile = false;
        return this;
    }

    public Gift Build()
        => new(
            name: "Action Figure",
            size: _size,
            isFragile: _isFragile,
            recommendedMinAge: 5);
}