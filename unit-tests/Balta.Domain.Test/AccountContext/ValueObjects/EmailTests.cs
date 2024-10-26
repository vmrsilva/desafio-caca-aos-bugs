using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.Test.Mocks.DateTimeProvider;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{
    [Theory(DisplayName = "Should Lower Case Email")]
    [InlineData("FAKE@EMAIL.COM", "fake@email.com")]
    [InlineData("FaKe@EmAIL.COM", "fake@email.com")]
    [InlineData("Fake@EmaiL.COM", "fake@email.com")]
    public void ShouldLowerCaseEmail(string email, string expectedEmail)
    {
        var fakeDate = DateTimeProviderFake.Create();

        var result = Email.ShouldCreate(email, fakeDate);

        Assert.Equal(expectedEmail, result.Address);
    }

    [Theory(DisplayName = "Should Trim Email")]
    [InlineData(" fake@email.com", "fake@email.com")]
    [InlineData("  fake@email.com ", "fake@email.com")]
    [InlineData("fake@email.com ", "fake@email.com")]
    public void ShouldTrimEmail(string email, string expectedEmail)
    {
        var fakeDate = DateTimeProviderFake.Create();

        var result = Email.ShouldCreate(email, fakeDate);

        Assert.Equal(expectedEmail, result.Address);
    }

    [Fact(DisplayName = "Should Fail If Email Is Null")]
    public void ShouldFailIfEmailIsNull()
    {
        var fakeDate = DateTimeProviderFake.Create();

        string fakeEmail = null;

        Assert.Throws<NullReferenceException>(() => Email.ShouldCreate(fakeEmail, fakeDate));
    }

    [Fact(DisplayName = "Should Fail If Email Is Empty")]
    public void ShouldFailIfEmailIsEmpty()
    {
        var fakeDate = DateTimeProviderFake.Create();

        string fakeEmail = string.Empty;

        Assert.Throws<InvalidEmailException>(() => Email.ShouldCreate(fakeEmail, fakeDate));
    }

    [Theory(DisplayName = "Should Fail If Email Is Invalid")]
    [InlineData("fake@email")]
    [InlineData("fakeemail.com")]
    [InlineData("fake#email.com")]
    [InlineData("fake!email.com")]
    public void ShouldFailIfEmailIsInvalid(string fakeEmail)
    {
        var fakeDate = DateTimeProviderFake.Create();

        Assert.Throws<InvalidEmailException>(() => Email.ShouldCreate(fakeEmail, fakeDate));
    }


    [Theory(DisplayName = "Should Pass If Email Is Valid")]
    [InlineData("fake@email.com.br")]
    [InlineData("teste@gmail.com")]
    [InlineData("teste@hotmail.com")]
    [InlineData("teste@hotmail.com.ar")]
    public void ShouldPassIfEmailIsValid(string fakeEmail)
    {
        var fakeDate = DateTimeProviderFake.Create();

        var result = Email.ShouldCreate(fakeEmail, fakeDate);

        Assert.NotNull(result);
    }

    [Theory(DisplayName = "Should Hash Email Address")]
    [InlineData("test@email.com")]
    [InlineData("test@fakemail.com")]
    [InlineData("test@fakemail.com.br")]
    public void ShouldHashEmailAddress(string fakeEmail)
    {
        var fakeDate = DateTimeProviderFake.Create();
        var expectedHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(fakeEmail));

        var result = Email.ShouldCreate(fakeEmail, fakeDate);

        Assert.Equal(expectedHash, result.Hash);
    }

    [Fact(DisplayName = "Should Explicit Convert From String")]
    public void ShouldExplicitConvertFromString()
    {
        var fakeDate = DateTimeProviderFake.Create();

        var fakeEmail = "fake@email.com";
        var result = Email.ShouldCreate(fakeEmail, fakeDate);

        string emailAsString = result;

        Assert.Equal(fakeEmail, emailAsString);
    }

    [Fact(DisplayName = "Should Explicit Convert To String")]
    public void ShouldExplicitConvertToString()
    {
        var fakeDate = DateTimeProviderFake.Create();

        var fakeEmail = "fake@email.com";
        var result = Email.ShouldCreate(fakeEmail, fakeDate);

        var emailAsString = Convert.ToString(result);

        Assert.Equal(fakeEmail, emailAsString);
    }

    [Fact(DisplayName = "ShouldReturn Email When Call To String Method")]
    public void ShouldReturnEmailWhenCallToStringMethod()
    {
        var fakeDate = DateTimeProviderFake.Create();

        var fakeEmail = "fake@email.com";
        var result = Email.ShouldCreate(fakeEmail, fakeDate);

        var emailAsString = result.ToString();

        Assert.Equal(fakeEmail, emailAsString);
    }
}