namespace Password;

public static class PasswordValidation
{
    public static bool Validate(string? password) => new ElfPasswordPolicy().Validate(password);
}