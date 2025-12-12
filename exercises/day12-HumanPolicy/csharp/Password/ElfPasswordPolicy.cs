namespace Password;

public class ElfPasswordPolicy
{
    private const int MinimumLength = 6;

    public bool Validate(string? password)
    {
        if (password == null) return false;
        if (password.Length < MinimumLength) return false;

        var uppercaseCount = 0;
        var digitCount = 0;

        foreach (var c in password)
        {
            if (char.IsUpper(c))
            {
                uppercaseCount++;
            }

            if (char.IsDigit(c))
            {
                digitCount++;
            }
        }

        return uppercaseCount >= 1 && digitCount == 1;
    }
}