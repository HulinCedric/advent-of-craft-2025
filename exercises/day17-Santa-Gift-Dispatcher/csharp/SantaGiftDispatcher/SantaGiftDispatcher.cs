using LanguageExt;
using static LanguageExt.Prelude;

namespace SantaGiftDispatcher;

/// <summary>
///     Dispatches gifts to children based on workshop inventory and each child's ordered wishlist.
/// </summary>
public sealed class SantaGiftDispatcher
{
    private readonly List<ChildWishlistRequest> _children;
    private readonly WorkshopInventory _inventory;

    public SantaGiftDispatcher(IDictionary<string, int> initialInventory)
    {
        _children = [];
        _inventory = WorkshopInventory.FromDictionary(initialInventory);
    }

    /// <summary>
    ///     Registers a child and their ordered wishlist. Children are processed in registration order.
    ///     The wishlist is copied defensively.
    /// </summary>
    public void RegisterChild(string childName, IEnumerable<string> wishlist)
        => _children.Add(
            new ChildWishlistRequest(
                childName,
                wishlist.Select(giftName => new Gift(giftName)).ToList()));

    /// <summary>
    ///     Assigns up to <paramref name="maxGiftsPerChild" /> gifts per child.
    ///     For each gift slot we try the child's wishlist in order; if nothing is available,
    ///     we use any remaining stock.
    /// </summary>
    public IReadOnlyList<GiftAssignment> Dispatch(int maxGiftsPerChild)
    {
        if (maxGiftsPerChild <= 0) return [];

        return _children.Fold(
            new Lst<GiftAssignment>(),
            (giftAssignments, child) => giftAssignments.AddRange(AssignGiftsForChild(child, maxGiftsPerChild)));
    }

    private Lst<GiftAssignment> AssignGiftsForChild(ChildWishlistRequest child, int maxGiftsPerChild)
        => Enumerable.Repeat(child, maxGiftsPerChild)
            .Fold(
                new Lst<GiftAssignment>(),
                (giftAssignments, childRequest)
                    => _inventory
                        .PickOneGiftFor(childRequest)
                        .Match(
                            Some: giftAssignments.Add,
                            None: () => giftAssignments));

    private sealed record Gift(string Name);

    private sealed record ChildWishlistRequest(string ChildName, IReadOnlyList<Gift> Wishlist);

    public sealed record GiftAssignment(string ChildName, string GiftName)
    {
        public override string ToString() => $"{ChildName} -> {GiftName}";
    }

    private sealed class WorkshopInventory
    {
        private HashMap<Gift, int> _remainingByGift;

        private WorkshopInventory(HashMap<Gift, int> remainingByGift) => _remainingByGift = remainingByGift;

        public static WorkshopInventory FromDictionary(IDictionary<string, int> initialInventory)
            => new(toHashMap(initialInventory.ToDictionary(kvp => new Gift(kvp.Key), kvp => kvp.Value)));

        public Option<GiftAssignment> PickOneGiftFor(ChildWishlistRequest request)
        {
            var giftInStock = AvailableWishedGiftsInOrder(request.Wishlist)
                .Append(FirstRemainingGiftInStock())
                .Somes()
                .HeadOrNone();

            PickOneInInventory(giftInStock);

            return giftInStock.Map(pickedGift => new GiftAssignment(request.ChildName, pickedGift.Name));
        }

        private IEnumerable<Option<Gift>> AvailableWishedGiftsInOrder(IReadOnlyList<Gift> wishedGifts)
            => wishedGifts.Map(WishedGiftInStock);

        private Option<Gift> WishedGiftInStock(Gift wishedGift)
            => _remainingByGift
                .Find(gift => gift.Key == wishedGift && gift.Value > 0)
                .Map(gift => gift.Key);

        private Option<Gift> FirstRemainingGiftInStock()
            => _remainingByGift
                .Find(gift => gift.Value > 0)
                .Map(gift => gift.Key);

        private void PickOneInInventory(Option<Gift> potentialGift)
            => _remainingByGift = potentialGift
                .Map(gift => _remainingByGift.AddOrUpdate(gift, availableInStock => availableInStock - 1, 0))
                .IfNone(_remainingByGift);
    }
}