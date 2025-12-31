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
        private readonly Dictionary<string, int> _remainingByGift;

        private WorkshopInventory(Dictionary<string, int> remainingByGift) => _remainingByGift = remainingByGift;

        public static WorkshopInventory FromDictionary(IDictionary<string, int> initialInventory)
            => new(new Dictionary<string, int>(initialInventory));

        public Option<string> PickOnePotentialGiftFor(ChildWishlistRequest child)
        {
            // 1) Wishlist in order.
            foreach (var wishedGift in child.Wishlist)
            {
                var picked = TakeOne(wishedGift);
                if (picked.IsSome) return picked;
            }

            // 2) Fallback: anything still in stock.
            return TakeAnyOne();
        }

        private Option<string> TakeOne(string gift)
        {
            if (!_remainingByGift.TryGetValue(gift, out var count) || count <= 0) return None;

            _remainingByGift[gift] = count - 1;
            return gift;
        }

        private Option<string> TakeAnyOne()
        {
            foreach (var kvp in _remainingByGift)
            {
                if (kvp.Value > 0)
                {
                    _remainingByGift[kvp.Key] = kvp.Value - 1;
                    return kvp.Key;
                }
            }

            return None;
        }
    }
}