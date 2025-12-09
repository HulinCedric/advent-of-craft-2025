namespace SantaMarket.Model
{
    public class ChristmasElf(ISantamarketCatalog catalog)
    {
        private readonly Dictionary<Product, Offer> _offers = new();

        public void AddSpecialOffer(SpecialOfferType offerType, Product product, double argument)
            => _offers[product] = new Offer(offerType, argument);

        public Receipt ChecksOutArticlesFrom(ShoppingSleigh shoppingSleigh)
        {
            var receipt = new Receipt();
            var productQuantities = shoppingSleigh.Items();

            foreach (var productQuantity in productQuantities)
            {
                var product = productQuantity.Product;
                var quantity = productQuantity.Quantity;
                var unitPrice = catalog.GetUnitPrice(product);
                var price = quantity * unitPrice;
                receipt.AddProduct(product, quantity, unitPrice, price);
            }

            shoppingSleigh.HandleOffers(receipt, _offers, catalog);

            return receipt;
        }
    }
}