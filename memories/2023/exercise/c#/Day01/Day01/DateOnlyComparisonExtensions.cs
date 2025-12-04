namespace Day01;

internal static class DateOnlyComparisonExtensions
{
    extension(DateOnly date)
    {
        public bool IsAfter(DateOnly other) 
            => date.CompareTo(other) > 0;
    }
}