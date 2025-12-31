using LanguageExt;
using static LanguageExt.Prelude;

namespace SantaGiftDispatcher;

/// <summary>
///     Dispatches gifts to children based on workshop inventory and each child's ordered wishlist.
/// </summary>
public sealed class SantaGiftDispatcher
{
    private readonly List<ChildWishlist> _childrenWishlist;
    private readonly WorkshopInventory _inventory;

    public SantaGiftDispatcher(IDictionary<string, int> initialInventory)
    {
        _childrenWishlist = [];
        _inventory = WorkshopInventory.FromDictionary(initialInventory);
    }

    /// <summary>
    ///     Registers a child and their ordered wishlist. Children are processed in registration order.
    ///     The wishlist is copied defensively.
    /// </summary>
    public void RegisterChild(string childName, IEnumerable<string> wishedGiftNames)
        => _childrenWishlist.Add(
            new ChildWishlist(
                childName,
                wishedGiftNames.Select(giftName => new Gift(giftName)).ToList()));

    /// <summary>
    ///     Assigns up to <paramref name="maxGiftsPerChild" /> gifts per child.
    ///     For each gift slot we try the child's wishlist in order; if nothing is available,
    ///     we use any remaining stock.
    /// </summary>
    public IReadOnlyList<GiftAssignment> Dispatch(int maxGiftsPerChild) => DispatchForAllChildren(maxGiftsPerChild);

    private Lst<GiftAssignment> DispatchForAllChildren(int maxGiftsPerChild)
        => _childrenWishlist.Fold(
            new Lst<GiftAssignment>(),
            (giftAssignments, child) => DispatchForChild(giftAssignments, child, maxGiftsPerChild));

    private Lst<GiftAssignment> DispatchForChild(
        Lst<GiftAssignment> giftAssignments,
        ChildWishlist child,
        int maxGiftsPerChild)
        => giftAssignments.AddRange(AssignGiftsForChild(child, maxGiftsPerChild));

    private Lst<GiftAssignment> AssignGiftsForChild(ChildWishlist wishList, int maxGiftsPerChild)
        => GiftSlots(maxGiftsPerChild)
            .Fold(
                new Lst<GiftAssignment>(),
                (giftAssignments, _) => AssignGiftForChild(giftAssignments, wishList));

    private static IEnumerable<Unit> GiftSlots(int maxGiftsPerChild) => Enumerable.Repeat(unit, maxGiftsPerChild);

    private Lst<GiftAssignment> AssignGiftForChild(Lst<GiftAssignment> giftAssignments, ChildWishlist child)
        => PickOneGiftForChild(child)
            .Match(
                Some: giftAssignments.Add,
                None: () => giftAssignments);

    private Option<GiftAssignment> PickOneGiftForChild(ChildWishlist child) => _inventory.PickOneGift(child);

    private sealed record Gift(string Name);

    private sealed record ChildWishlist(string ChildName, IReadOnlyList<Gift> OrderedWishedGifts);

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

        public Option<GiftAssignment> PickOneGift(ChildWishlist wishlist)
        {
            var giftInStock = AvailableWishedGiftsInOrder(wishlist.OrderedWishedGifts)
                .Append(FirstRemainingGiftInStock())
                .Somes()
                .HeadOrNone();

            PickOneInInventory(giftInStock);

            return giftInStock.Map(pickedGift => new GiftAssignment(wishlist.ChildName, pickedGift.Name));
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