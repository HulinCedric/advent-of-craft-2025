using System.Collections;

namespace SantaGiftDispatcher;

/// <summary>
/// Dispatches gifts to children based on workshop inventory and each child's ordered wishlist.
/// </summary>
public sealed class SantaGiftDispatcher
{
    private readonly WorkshopInventory _inventory;
    private readonly List<ChildWishlistRequest> _registeredChildren = new();

    public SantaGiftDispatcher(IDictionary initialInventory)
    {
        _inventory = WorkshopInventory.FromDictionary(initialInventory);
    }

    /// <summary>
    /// Registers a child and their ordered wishlist. Children are processed in registration order.
    /// The wishlist is copied defensively.
    /// </summary>
    public void RegisterChild(string childName, IList wishlist)
    {
        if (childName == null || wishlist == null)
        {
            return;
        }

        var copiedWishlist = new List<object>(wishlist.Count);
        foreach (var gift in wishlist)
        {
            copiedWishlist.Add(gift);
        }

        _registeredChildren.Add(new ChildWishlistRequest(childName, copiedWishlist));
    }

    /// <summary>
    /// Assigns up to <paramref name="maxGiftsPerChild"/> gifts per child.
    /// For each gift slot we try the child's wishlist in order; if nothing is available,
    /// we use any remaining stock.
    /// </summary>
    public IReadOnlyList<GiftAssignment> Dispatch(int maxGiftsPerChild)
    {
        var assignments = new List<GiftAssignment>();

        if (maxGiftsPerChild <= 0)
        {
            return assignments;
        }

        foreach (var child in _registeredChildren)
        {
            for (int remainingSlots = maxGiftsPerChild; remainingSlots > 0; remainingSlots--)
            {
                if (!TryPickOneGiftFor(child, out var pickedGift))
                {
                    break;
                }

                assignments.Add(new GiftAssignment(child.ChildName, pickedGift!.ToString()!));
            }
        }

        return assignments;
    }

    private bool TryPickOneGiftFor(ChildWishlistRequest child, out object? pickedGift)
    {
        pickedGift = null;

        // 1) Wishlist in order.
        foreach (var wishedGift in child.Wishlist)
        {
            if (wishedGift == null)
            {
                continue;
            }

            if (_inventory.TryTakeOne(wishedGift))
            {
                pickedGift = wishedGift;
                return true;
            }
        }

        // 2) Fallback: anything still in stock.
        if (_inventory.TryTakeAnyOne(out var anyGift))
        {
            pickedGift = anyGift;
            return true;
        }

        return false;
    }

    private sealed record ChildWishlistRequest(string ChildName, IReadOnlyList<object?> Wishlist);

    public sealed record GiftAssignment(string ChildName, string Gift)
    {
        public override string ToString() => $"{ChildName} -> {Gift}";
    }

    private sealed class WorkshopInventory
    {
        private readonly Dictionary<object, int> _remainingByGift;

        private WorkshopInventory(Dictionary<object, int> remainingByGift)
        {
            _remainingByGift = remainingByGift;
        }

        public static WorkshopInventory FromDictionary(IDictionary initialInventory)
        {
            var remaining = new Dictionary<object, int>();

            if (initialInventory != null)
            {
                foreach (DictionaryEntry entry in initialInventory)
                {
                    // Preserve original "loose" behavior: non-int counts become unusable (0).
                    remaining[entry.Key] = entry.Value is int count ? count : 0;
                }
            }

            return new WorkshopInventory(remaining);
        }

        public bool TryTakeOne(object giftKey)
        {
            if (!_remainingByGift.TryGetValue(giftKey, out var count) || count <= 0)
            {
                return false;
            }

            _remainingByGift[giftKey] = count - 1;
            return true;
        }

        public bool TryTakeAnyOne(out object? giftKey)
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
    }
}