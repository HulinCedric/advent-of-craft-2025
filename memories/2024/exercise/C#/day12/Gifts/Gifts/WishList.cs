namespace Gifts;

internal class WishList : List<Toy>
{
    public Toy LastChoice() => this[^1];

    public Toy? SecondChoice() => this[1];

    public Toy? FirstChoice() => this[0];
}