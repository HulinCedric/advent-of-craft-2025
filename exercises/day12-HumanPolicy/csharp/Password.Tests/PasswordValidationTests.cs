using FluentAssertions;
using Xunit;

namespace Password.Tests;

public static class PasswordValidationTests
{
    public class ForElf
    {
        public static IEnumerable<object[]> ValidElfPasswords
            => new List<object[]>
            {
                new object[] { "Abcde1" },
                new object[] { "ELfMAr1" },
                new object[] { "XmasEL1" },
                new object[] { "aBcdef1" }
            };

        public static IEnumerable<object[]> InvalidElfPasswords
            => new List<object[]>
            {
                new object[] { "", "Too short" },
                new object[] { "Abc1", "Too short" },
                new object[] { "abcdef", "No uppercase, no digit" },
                new object[] { "abcde1", "No uppercase" },
                new object[] { "ABCDEF", "No digit" },
                new object[] { "Abcde12", "More than one digit" },
                new object[] { "abcdE2f3", "More than one digit" },
                new object[] { null, "Null password" }
            };

        [Theory]
        [MemberData(nameof(ValidElfPasswords))]
        public void Success_for_a_valid_elf_password(string password)
            => PasswordValidation.Validate(password, new ElfPasswordPolicy()).Should().BeTrue();

        [Theory]
        [MemberData(nameof(InvalidElfPasswords))]
        public void Invalid_elf_passwords(string password, string reason)
            => PasswordValidation.Validate(password, new ElfPasswordPolicy())
                .Should()
                .BeFalse(reason);
    }
    
    public class ForHuman
    {
        [Theory]
        [InlineData("P@ssw0rd")]
        [InlineData("Advent0fCraft&")]
        public void Success_for_a_valid_human_password(string password)
            => PasswordValidation.Validate(password, new HumanPasswordPolicy()).Should().BeTrue();

        [Theory]
        [InlineData(null, "Null password")]
        [InlineData("xxxxxxx", "Too short")]
        [InlineData("adventofcraft", "No capital letter")]
        [InlineData("p@ssw0rd", "No capital letter")]
        [InlineData("ADVENTOFCRAFT", "No lowercase letter")]
        [InlineData("P@SSW0RD", "No lowercase letter")]
        [InlineData("Adventofcraft", "No number")]
        [InlineData("P@sswOrd", "No number")]
        [InlineData("Adventof09craft", "No special character")]
        [InlineData("PAssw0rd", "No special character")]
        [InlineData("Advent@of9Craft¨", "Invalid character")]
        [InlineData("P@ssw^rd", "Invalid character")]
        public void Invalid_elf_passwords(string password, string reason)
            => PasswordValidation.Validate(password, new HumanPasswordPolicy())
                .Should()
                .BeFalse(reason);
    }
}

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