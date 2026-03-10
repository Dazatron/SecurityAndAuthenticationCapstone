using SecurityAndAuthenticationCapstone.Services;
using NUnit.Framework;

namespace SecurityAndAuthenticationCapstone.Tests
{
    [TestFixture]
    public class TestInputValidation
    {
        [Test]
        public void TestForSQLInjection()
        {
            string attack = "' OR 1=1 --";

            string result = InputSanitizer.SanitizeUsername(attack);

            Assert.IsFalse(result.Contains("'"));
            Assert.IsFalse(result.Contains("--"));
        }

        [Test]
        public void TestForXSS()
        {
            string attack = "<script>alert('XSS')</script>";

            string result = InputSanitizer.SanitizeEmail(attack);

            Assert.IsFalse(result.Contains("<script>"));
            Assert.IsTrue(result.Contains("&lt;script&gt;"));
        }
    }
}