using NUnit.Framework;
using SecurityAndAuthenticationCapstone.Services;

[TestFixture]
public class TestAuthentication
{
    [Test]
    public void InvalidPasswordShouldFail()
    {
        string hash = BCrypt.Net.BCrypt.HashPassword("CorrectPassword");

        bool result = BCrypt.Net.BCrypt.Verify("WrongPassword", hash);

        Assert.IsFalse(result);
    }

    [Test]
    public void ValidPasswordShouldPass()
    {
        string hash = BCrypt.Net.BCrypt.HashPassword("MyPassword");

        bool result = BCrypt.Net.BCrypt.Verify("MyPassword", hash);

        Assert.IsTrue(result);
    }
}