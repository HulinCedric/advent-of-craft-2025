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

    // - It has **at least 8 characters**
    // - It contains **at least one uppercase letter**
    // - It contains **at least one lowercase letter**
    // - It contains **at least one digit**
    // - It contains **at least one special character** from this whitelist:
    //   - `.`, `*`, `#`, `@`, `$`, `%`, `&`
    // - It contains **no invalid characters**:
    //   - only letters, digits, and the special characters listed above are allowed.
    public class ForHuman
    {
        [Theory]
        [InlineData("P@ssw0rd")]
        [InlineData("Advent0fCraft&")]
        public void Success_for_a_valid_human_password(string password)
            => PasswordValidation.Validate(password, new HumanPasswordPolicy()).Should().BeTrue();

        [Theory]
        [InlineData(null, "Null password")]
        [InlineData("xxxxxxx", "Too short", Skip = "TODO")]
        [InlineData("adventofcraft", "No capital letter", Skip = "TODO")]
        [InlineData("p@ssw0rd", "No capital letter", Skip = "TODO")]
        [InlineData("ADVENTOFCRAFT", "No lowercase letter", Skip = "TODO")]
        [InlineData("P@SSW0RD", "No lowercase letter", Skip = "TODO")]
        [InlineData("Adventofcraft", "No number", Skip = "TODO")]
        [InlineData("P@sswOrd", "No number", Skip = "TODO")]
        [InlineData("Adventof09craft", "No special character", Skip = "TODO")]
        [InlineData("PAssw0rd", "No special character", Skip = "TODO")]
        [InlineData("Advent@of9Craft¨", "Invalid character", Skip = "TODO")]
        [InlineData("P@ssw^rd", "Invalid character", Skip = "TODO")]
        public void Invalid_elf_passwords(string password, string reason)
            => PasswordValidation.Validate(password, new HumanPasswordPolicy())
                .Should()
                .BeFalse(reason);
    }
}

public class HumanPasswordPolicy : IPasswordPolicy
{
    public bool Validate(string? password)
    {
        if (password is null) return false;
        
        return true;
    }
}