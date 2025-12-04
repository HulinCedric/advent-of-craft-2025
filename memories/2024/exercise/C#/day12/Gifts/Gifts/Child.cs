namespace Gifts;

public class Child(string name, BehaviorType behavior)
{
    public string Name { get; } = name;
    private BehaviorType Behavior { get; } = behavior;
    private WishList Wishlist { get; set; } = [];

    public void SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
        => Wishlist = [firstChoice, secondChoice, thirdChoice];

    public Toy? GetDeservedToy() 
        => Behavior switch
        {
            BehaviorType.NAUGHTY => Wishlist.LastChoice(),
            BehaviorType.NICE => Wishlist.SecondChoice(),
            BehaviorType.VERY_NICE => Wishlist.FirstChoice(),
            _ => null
        };
}