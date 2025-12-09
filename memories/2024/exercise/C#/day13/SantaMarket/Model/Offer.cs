namespace SantaMarket.Model;

public class Offer(SpecialOfferType offerType, double argument)
{
    public SpecialOfferType OfferType { get; } = offerType;
    public double Argument { get; } = argument;

    public Discount? CalculateDiscount(
        Product product,
        double unitPrice,
        int quantityAsInt)
    {
        if (OfferType == SpecialOfferType.TwoForAmount && quantityAsInt >= 2)
        {
            var total = Argument * (quantityAsInt / 2) + (quantityAsInt % 2) * unitPrice;
            return new Discount(product, "2 for " + Argument, -(unitPrice * quantityAsInt - total));
        }

        if (OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
        {
            var discountAmount = quantityAsInt * unitPrice -
                                 ((quantityAsInt / 3 * 2 * unitPrice) + (quantityAsInt % 3) * unitPrice);
            return new Discount(product, "3 for 2", -discountAmount);
        }

        if (OfferType == SpecialOfferType.TenPercentDiscount) return CalculateTenPercentDiscount(product, unitPrice, quantityAsInt);

        if (OfferType == SpecialOfferType.FiveForAmount)
        {
            if (quantityAsInt >= 5)
            {
                var discountTotal = unitPrice * quantityAsInt -
                                    (Argument * (quantityAsInt / 5) + (quantityAsInt % 5) * unitPrice);
                return new Discount(product, "5 for " + Argument, -discountTotal);
            }
        }

        return null;
    }

    private Discount? CalculateTenPercentDiscount(Product product, double unitPrice, int quantityAsInt) => new(
        product,
        Argument + "% off",
        -quantityAsInt * unitPrice * Argument / 100.0);
}