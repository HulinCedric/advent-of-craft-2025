namespace SantaGiftDispatcher;

/// <summary>
///     Dispatches gifts to children based on workshop inventory and each child's ordered wishlist.
/// </summary>
public sealed class SantaGiftDispatcher
{
    private readonly WorkshopInventory _inventory;
    private readonly List<ChildWishlistRequest> _registeredChildren = new();

    public SantaGiftDispatcher(IDictionary<string, int> initialInventory)
        => _inventory = WorkshopInventory.FromDictionary(initialInventory);

    /// <summary>
    ///     Registers a child and their ordered wishlist. Children are processed in registration order.
    ///     The wishlist is copied defensively.
    /// </summary>
    public void RegisterChild(string childName, IEnumerable<string> wishlist)
        => _registeredChildren.Add(new ChildWishlistRequest(childName, new List<string>(wishlist)));

    /// <summary>
    ///     Assigns up to <paramref name="maxGiftsPerChild" /> gifts per child.
    ///     For each gift slot we try the child's wishlist in order; if nothing is available,
    ///     we use any remaining stock.
    /// </summary>
    public IReadOnlyList<GiftAssignment> Dispatch(int maxGiftsPerChild)
    {
        var assignments = new List<GiftAssignment>();

        if (maxGiftsPerChild <= 0) return assignments;

        foreach (var child in _registeredChildren)
        {
            assignments.AddRange(AssignGifts(child, maxGiftsPerChild));
        }

        return assignments;
    }

    private IEnumerable<GiftAssignment> AssignGifts(ChildWishlistRequest child, int maxGiftsPerChild)
    {
        var childAssignments = new List<GiftAssignment>();
        for (var remainingSlots = maxGiftsPerChild; remainingSlots > 0; remainingSlots--)
        {
            var giftAssignment = AssignGift(child);
            if (giftAssignment is null) break;

            childAssignments.Add(giftAssignment);
        }

        return childAssignments;
    }

    private GiftAssignment? AssignGift(ChildWishlistRequest child)
    {
        var pickedGift = _inventory.PickOnePotentialGiftFor(child);
        return pickedGift is null
            ? null
            : new GiftAssignment(child.ChildName, pickedGift);
    }

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

        private bool TryTakeOne(string giftKey)
        {
            if (!_remainingByGift.TryGetValue(giftKey, out var count) || count <= 0) return false;

            _remainingByGift[giftKey] = count - 1;
            return true;
        }

        private bool TryTakeAnyOne(out string? giftKey)
        {
            foreach (var kvp in _remainingByGift)
            {
                if (kvp.Value > 0)
                {
                    _remainingByGift[kvp.Key] = kvp.Value - 1;
                    giftKey = kvp.Key;
                    return true;
                }
            }

            giftKey = null;
            return false;
        }

        public string? PickOnePotentialGiftFor(ChildWishlistRequest child)
        {
            // 1) Wishlist in order.
            foreach (var wishedGift in child.Wishlist)
            {
                if (TryTakeOne(wishedGift)) return wishedGift;
            }

            // 2) Fallback: anything still in stock.
            if (TryTakeAnyOne(out var anyGift)) return anyGift;

            return null;
        }
    }
}