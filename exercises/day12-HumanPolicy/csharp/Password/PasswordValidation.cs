namespace Password;

public static class PasswordValidation
{
    public static bool Validate(string? password) => Validate(password, new ElfPasswordPolicy());

    public static bool Validate(string? password, IPasswordPolicy policy) => policy.Validate(password);
}