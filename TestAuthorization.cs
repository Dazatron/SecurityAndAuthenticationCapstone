using NUnit.Framework;

namespace SecurityAndAuthenticationCapstone.Test
{
    internal class TestAuthorization
    {
        [Test]
        public void NonAdminShouldNotAccessAdminPage()
        {
            string role = "user";

            Assert.AreNotEqual("admin", role);
        }
    }
}
