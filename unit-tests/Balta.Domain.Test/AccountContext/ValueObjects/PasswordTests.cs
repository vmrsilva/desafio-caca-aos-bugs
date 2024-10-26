using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.Test.Mocks.Password;

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
        string fakePassword = PasswordFake.GenerateFakePassword(7);

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(fakePassword));
    }

    [Fact(DisplayName = "Should Fail If Password Len Is Greater Than Max Chars")]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars()
    {
        string fakePassword = PasswordFake.GenerateFakePassword(50);

        Assert.Throws<InvalidPasswordException>(() => Password.ShouldCreate(fakePassword));
    }


    [Fact(DisplayName = "Should Hash Password")]
    public void ShouldHashPassword()
    {
        string fakePassword = PasswordFake.GenerateFakePassword(9);

        var result = Password.ShouldCreate(fakePassword);

        Assert.NotEmpty(result.Hash);
    }

    [Fact(DisplayName = "Should Verify Password Hash")]
    public void ShouldVerifyPasswordHash()
    {
        var fakePassword = PasswordFake.GenerateFakePassword(9);

        var result = Password.ShouldCreate(fakePassword);

        Assert.True(Password.ShouldMatch(result.Hash, fakePassword));
    }

    [Fact]
    public void ShouldGenerateStrongPassword()
    {
       var result = Password.ShouldGenerate(includeSpecialChars: true, upperCase: true);

        //TODO 
        
    }

    [Fact]
    public void ShouldImplicitConvertToString() => Assert.Fail();

    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod() => Assert.Fail();

    [Fact]
    public void ShouldMarkPasswordAsExpired() => Assert.Fail();

    [Fact]
    public void ShouldFailIfPasswordIsExpired() => Assert.Fail();

    [Fact]
    public void ShouldMarkPasswordAsMustChange() => Assert.Fail();

    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange() => Assert.Fail();
}