using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using Balta.Domain.Test.Mocks.DateTimeProvider;
using FakeItEasy;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class VerificationCodeTest
{
    private const int MinLength = 6;

    [Fact(DisplayName = "Should Generate Verification Code")]
    public void ShouldGenerateVerificationCode()
    {
        var fakeDate = FakeDateTimeProvider.Default;

        var result = VerificationCode.ShouldCreate(fakeDate);

        Assert.NotNull(result);
        Assert.Equal(MinLength, result.Code.Length);
    }

    [Fact(DisplayName = "Should Generate Expires At In Future")]
    public void ShouldGenerateExpiresAtInFuture()
    {
        var fakeDate = FakeDateTimeProvider.Default;

        var result = VerificationCode.ShouldCreate(fakeDate);

        Assert.True(result.ExpiresAtUtc > fakeDate.UtcNow);
    }

    [Fact(DisplayName = "Should Generate Verified At As Null")]
    public void ShouldGenerateVerifiedAtAsNull()
    {
        var fakeDate = FakeDateTimeProvider.Default;

        var result = VerificationCode.ShouldCreate(fakeDate);

        Assert.Null(result.VerifiedAtUtc);
    }

    //Change the test from Should Be Inactive When Created to Should Be Active When Created
    //Due password always is created Active.
    [Fact(DisplayName = "Should Be Active When Created")]
    public void ShouldBeActiveWhenCreated()
    {
        
        var fakeDate = FakeDateTimeProvider.Default;
        
        var verificationCode = VerificationCode.ShouldCreate(fakeDate);

        Assert.True(verificationCode.IsActive);
    }

    [Fact(DisplayName = "Should Fail If Expired")]
    public void ShouldFailIfExpired()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.False(result.IsExpired);

    }

    [Fact(DisplayName = "Should Fail If Code Is Invalid")]
    public void ShouldFailIfCodeIsInvalid()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.NotEmpty(result.Code);
    }

    [Fact(DisplayName = "Should Fail If Code Is Less Than Six Chars")]
    public void ShouldFailIfCodeIsLessThanSixChars()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.False(result.Code.Length < 6);
    }

    [Fact(DisplayName = "Should Fail If Code Is Greater Than Six Chars")]
    public void ShouldFailIfCodeIsGreaterThanSixChars()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.False(result.Code.Length > 6);
    }

    [Fact(DisplayName = "Should Fail If Is Not Active")]
    public void ShouldFailIfIsNotActive()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.True(result.IsActive);
    }

    [Fact(DisplayName = "ShouldF ailIf Is Already Verified")]
    public void ShouldFailIfIsAlreadyVerified()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.Null(result.VerifiedAtUtc);
    }

    [Fact(DisplayName = "Should Fail If Is Verification Code Does Not Match")]
    public void ShouldFailIfIsVerificationCodeDoesNotMatch()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.Matches(@"[a-zA-Z0-9]{6}", result.Code);
    }

    [Fact(DisplayName = "Should Verify")]
    public void ShouldVerify()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        result.ShouldVerify("abc123");

        Assert.NotNull(result.VerifiedAtUtc);
    }
}