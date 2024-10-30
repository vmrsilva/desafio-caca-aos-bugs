using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.Test.Mocks.DateTimeProvider;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    private const string specialCharacter = "!@#$%^&*(){}[];";

    [Fact(DisplayName = "Should Fail If Password Is Null")]
    public void ShouldFailIfPasswordIsNull()
    {
        string fakePassword = null;

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(fakePassword));
    }

    [Fact(DisplayName = "Should Fail If Password Is Empty")]
    public void ShouldFailIfPasswordIsEmpty()
    {
        string fakePassword = string.Empty;

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(fakePassword));
    }

    [Fact(DisplayName = "Should Fail If Password Is White Space")]
    public void ShouldFailIfPasswordIsWhiteSpace()
    {
        string fakePassword = "   ";

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(fakePassword));
    }

    [Fact(DisplayName = "Should Fail If Password Len Is Less Than Minimum Chars")]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars()
    {
        string fakePassword = Password.ShouldGenerate(7);

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(fakePassword));
    }

    [Fact(DisplayName = "Should Fail If Password Len Is Greater Than Max Chars")]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars()
    {
        string fakePassword = Password.ShouldGenerate(50);

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(fakePassword));
    }


    [Fact(DisplayName = "Should Hash Password")]
    public void ShouldHashPassword()
    {
        string fakePassword = Password.ShouldGenerate(9);

        var result = Password.ShouldCreate(fakePassword);

        Assert.NotEmpty(result.Hash);
    }

    [Fact(DisplayName = "Should Verify Password Hash")]
    public void ShouldVerifyPasswordHash()
    {
        var fakePassword = Password.ShouldGenerate(9);

        var result = Password.ShouldCreate(fakePassword);

        Assert.True(Password.ShouldMatch(result.Hash, fakePassword));
    }

    [Fact(DisplayName = "Should Generate Strong Password")]
    public void ShouldGenerateStrongPassword()
    {
        bool IsSpecialCharacter(char c)
        {
            return !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c);
        }

        var result = Password.ShouldGenerate();

        Assert.True(
            result.Any(char.IsNumber) &&
            result.Any(char.IsLetter) &&
            result.Any(IsSpecialCharacter));

    }

    [Fact]
    public void ShouldImplicitConvertToString()
    {
        var fakePasswor = Password.ShouldGenerate(8);
        var result = Password.ShouldCreate(fakePasswor);

        var passwordHash = Convert.ToString(result);

        Assert.Equal(result.Hash, passwordHash);
    }

    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod()
    {
        var fakePasswor = Password.ShouldGenerate(8);
        var result = Password.ShouldCreate(fakePasswor);

        var passwordHash = result.ToString();

        Assert.IsType<string>(passwordHash);
        Assert.Equal(result.Hash, passwordHash);
    }

    [Fact(DisplayName = "Should Mark Password As Expired")]
    public void ShouldMarkPasswordAsExpired()
    {
        var provider = new FakeDateTimeProvider();

        var result = Password.ShouldCreate("validPass123!", provider);

        provider.ChangeDate(DateTime.UtcNow.AddMinutes(5.1));

        Assert.True(result.IsExpired());
    }

    [Fact(DisplayName = "Should Fail If Password Is Expired")]
    public void ShouldFailIfPasswordIsExpired()
    {
        var result = Password.ShouldCreate("validPass123!");

        Assert.False(result.IsExpired());
    }

    [Fact(DisplayName = "Should Mark Password As Must Change")]
    public void ShouldMarkPasswordAsMustChange()
    {
        var provider = new FakeDateTimeProvider();

        var result = Password.ShouldCreate("validPass123!", provider);

        provider.ChangeDate(DateTime.UtcNow.AddMinutes(5.1));

        Assert.True(result.MustChange);
    }

    [Fact(DisplayName = "Should Fail If Password Is Marked As Must Change")]
    public void ShouldFailIfPasswordIsMarkedAsMustChange()
    {
        var result = Password.ShouldCreate("validPass123!");

        Assert.False(result.MustChange);
    }
}