using NUnit.Framework;
using System;
using ToDoList.Modules;

namespace ToDoList.Test.Modules
{
    [TestFixture]
    class JWTAuthorizationTest
    {
        [Test]
        public void GetAuthTokenTest()
        {
            var key = new byte[] { 01, 02, 03, 04 };
            var jwt = new JWTAuthorization(key);
            var claims = new AuthClaims()
            {
                UserId = Guid.Parse("D9C18D24-9D4F-4BE3-BF3F-1D1A438B8EFB"),
                SessionId = Guid.Parse("0952B304-EDB1-40DC-96AB-C4575FE45848"),
            };
            var token = jwt.GetAuthToken(claims);
            Assert.NotNull(token);
        }

        [Test]
        public void ValidateAuthTest()
        {
            var key = new byte[] { 01, 02, 03, 04 };
            var jwt = new JWTAuthorization(key);
            var claims = new AuthClaims()
            {
                UserId = Guid.Parse("D9C18D24-9D4F-4BE3-BF3F-1D1A438B8EFB"),
                SessionId = Guid.Parse("0952B304-EDB1-40DC-96AB-C4575FE45848"),
            };
            var token = jwt.GetAuthToken(claims);
            var recoveredClaims = jwt.ValidateAuth(token);
            Assert.IsTrue(claims.UserId == recoveredClaims.UserId &&
                          claims.SessionId == recoveredClaims.SessionId);
        }
    }
}
