using LanguageExt;
using static LanguageExt.Prelude;

namespace SantaGiftDispatcher;

/// <summary>
///     Dispatches gifts to children based on workshop inventory and each child's ordered wishlist.
/// </summary>
public sealed class SantaGiftDispatcher
{
    private readonly List<ChildWishlistRequest> _children = new();
    private readonly WorkshopInventory _inventory;

    public SantaGiftDispatcher(IDictionary<string, int> initialInventory)
        => _inventory = WorkshopInventory.FromDictionary(initialInventory);

    /// <summary>
    ///     Registers a child and their ordered wishlist. Children are processed in registration order.
    ///     The wishlist is copied defensively.
    /// </summary>
    public void RegisterChild(string childName, IEnumerable<string> wishlist)
        => _children.Add(new ChildWishlistRequest(childName, new List<string>(wishlist)));

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
                    => AssignGiftForChild(childRequest)
                        .Match(
                            Some: giftAssignments.Add,
                            None: () => giftAssignments));

    private Option<GiftAssignment> AssignGiftForChild(ChildWishlistRequest child)
        => _inventory
            .PickOnePotentialGiftFor(child)
            .Map(pickedGift => new GiftAssignment(child.ChildName, pickedGift));

    private sealed record ChildWishlistRequest(string ChildName, IReadOnlyList<string> Wishlist);

    public sealed record GiftAssignment(string ChildName, string Gift)
    {
        public override string ToString() => $"{ChildName} -> {Gift}";
    }

    private sealed class WorkshopInventory
    {
        private Map<string, int> _remainingByGift;

        private WorkshopInventory(Map<string, int> remainingByGift) => _remainingByGift = remainingByGift;

        public static WorkshopInventory FromDictionary(IDictionary<string, int> initialInventory)
            => new(toMap(initialInventory));

        public Option<string> PickOnePotentialGiftFor(ChildWishlistRequest child)
        {
            var potentialGift =
                // 1) Wishlist in order.
                child.Wishlist.Map(PotentialWishedGift)
                    // 2) Fallback: anything still in stock.
                    .Append(PotentialAvailableGift())
                    .Somes()
                    .HeadOrNone();

            PickInInventory(potentialGift);

            return potentialGift.Map(gift => gift.Key);
        }

        private void PickInInventory(Option<(string Key, int Value)> potentialWishedGift)
            => _remainingByGift = potentialWishedGift
                .Map(gift => _remainingByGift.AddOrUpdate(gift.Key, gift.Value - 1))
                .IfNone(_remainingByGift);

        private Option<(string Key, int Value)> PotentialWishedGift(string giftName)
            => _remainingByGift
                .Find(gift => gift.Key == giftName && gift.Value > 0);

        private Option<(string Key, int Value)> PotentialAvailableGift()
        {
            var potentialAvailableGift = _remainingByGift
                .Find(gift => gift.Value > 0);
            return potentialAvailableGift;
        }
    }
}