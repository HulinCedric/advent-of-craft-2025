namespace Day01;

internal static class ExpirationDateExtensions
{
    extension(DateOnly expirationDate)
    {
        internal bool HasNotExpired(DateOnly now) => expirationDate > now;
    }
}