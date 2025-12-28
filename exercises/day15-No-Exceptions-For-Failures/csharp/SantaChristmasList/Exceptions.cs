namespace SantaChristmasList;

public class GiftNotManufacturedException : Exception
{
    public GiftNotManufacturedException(Gift gift)
        : base($"Gift has not been manufactured: {gift.Name}")
    {
    }
}

public class GiftOutOfStockException : Exception
{
    public GiftOutOfStockException(Gift gift)
        : base($"Gift out of stock: {gift.Name}")
    {
    }
}

public class BusinessException : Exception
{
    public BusinessException(string message, Exception inner)
        : base(message, inner)
    {
    }
}