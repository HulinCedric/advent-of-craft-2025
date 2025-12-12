namespace Password;

public class HumanPasswordPolicy : IPasswordPolicy
{
    private const int MinLength = 8;
    private static readonly HashSet<char> SpecialCharacters = ['.', '*', '#', '@', '$', '%', '&'];

    public bool Validate(string? password)
    {
        if (password is null) return false;
        if (password.Length < MinLength) return false;
        if (!password.Any(char.IsLower)) return false;
        if (!password.Any(char.IsUpper)) return false;
        if (!password.Any(char.IsNumber)) return false;
        if (!password.Any(c => SpecialCharacters.Contains(c))) return false;
        if (!password.All(c => char.IsLetterOrDigit(c) || SpecialCharacters.Contains(c))) return false;

        return true;
    }
}