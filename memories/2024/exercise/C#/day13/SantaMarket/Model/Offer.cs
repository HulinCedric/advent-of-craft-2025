namespace SantaMarket.Model
{
    public class Offer(SpecialOfferType offerType, double argument)
    {
        public SpecialOfferType OfferType { get; } = offerType;
        public double Argument { get; } = argument;

        public Discount? CalculateDiscount(
            int quantityAsInt,
            double unitPrice,
            Product product)
        {
            switch (OfferType)
            {
                case SpecialOfferType.TwoForAmount when quantityAsInt >= 2:
                {
                    var total = Argument * (quantityAsInt / 2) + (quantityAsInt % 2) * unitPrice;
                    return new Discount(product, "2 for " + Argument, -(unitPrice * quantityAsInt - total));
                }
                case SpecialOfferType.ThreeForTwo when quantityAsInt > 2:
                {
                    var discountAmount = quantityAsInt * unitPrice -
                                         ((quantityAsInt / 3 * 2 * unitPrice) + (quantityAsInt % 3) * unitPrice);
                    return new Discount(product, "3 for 2", -discountAmount);
                }
                case SpecialOfferType.TenPercentDiscount:
                    return new Discount(product, Argument + "% off",
                        -quantityAsInt * unitPrice * Argument / 100.0);
                case SpecialOfferType.FiveForAmount when quantityAsInt >= 5:
                {
                    var discountTotal = unitPrice * quantityAsInt -
                                        (Argument * (quantityAsInt / 5) + (quantityAsInt % 5) * unitPrice);
                    return new Discount(product, "5 for " + Argument, -discountTotal);
                }
                default:
                    return null;
            }
        }
    }
}