namespace Password;

public interface IPasswordPolicy
{
    bool Validate(string? password);
}