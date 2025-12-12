using FluentAssertions;
using Xunit;

namespace Password.Tests;

public class PasswordValidationTests
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