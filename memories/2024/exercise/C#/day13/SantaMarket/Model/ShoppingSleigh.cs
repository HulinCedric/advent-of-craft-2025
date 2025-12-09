namespace SantaMarket.Model
{
    public class ShoppingSleigh
    {
        private readonly List<ProductQuantity> _items = [];
        private readonly Dictionary<Product, double> _productQuantities = new();

        public IReadOnlyList<ProductQuantity> Items() => _items.AsReadOnly();

        public void AddItem(Product product) => AddItemQuantity(product, 1.0);

        public IReadOnlyDictionary<Product, double> ProductQuantities() => _productQuantities.AsReadOnly();

        public void AddItemQuantity(Product product, double quantity)
        {
            _items.Add(new ProductQuantity(product, quantity));
            if (_productQuantities.ContainsKey(product))
            {
                _productQuantities[product] += quantity;
            }
            else
            {
                _productQuantities[product] = quantity;
            }
        }

        public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, ISantamarketCatalog catalog)
        {
            foreach (var product in ProductQuantities().Keys)
            {
                var quantity = _productQuantities[product];
                if (offers.ContainsKey(product))
                {
                    var offer = offers[product];
                    var unitPrice = catalog.GetUnitPrice(product);
                    var quantityAsInt = (int) quantity;

                    var discount = offer.CalculateDiscount(product, unitPrice, quantityAsInt);

                    if (discount != null)
                    {
                        receipt.AddDiscount(discount);
                    }
                }
            }
        }
    }
}