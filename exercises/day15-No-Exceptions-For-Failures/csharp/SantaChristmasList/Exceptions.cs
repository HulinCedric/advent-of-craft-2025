namespace SantaChristmasList;

public class BusinessException : Exception
{
    public BusinessException(string message, Exception inner)
        : base(message, inner)
    {
    }
}