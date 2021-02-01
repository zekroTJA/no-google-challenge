using JWT.Exceptions;
using NUnit.Framework;
using System;
using System.Threading;
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
            var jwt = new JWTAuthorization(key, TimeSpan.FromMinutes(1));
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
            var jwt = new JWTAuthorization(key, TimeSpan.FromSeconds(1));
            var claims = new AuthClaims()
            {
                UserId = Guid.Parse("D9C18D24-9D4F-4BE3-BF3F-1D1A438B8EFB"),
                SessionId = Guid.Parse("0952B304-EDB1-40DC-96AB-C4575FE45848"),
            };
            var token = jwt.GetAuthToken(claims);
            var recoveredClaims = jwt.ValidateAuth(token);
            Assert.IsTrue(claims.UserId == recoveredClaims.UserId &&
                          claims.SessionId == recoveredClaims.SessionId);

            Assert.Throws<InvalidTokenPartsException>(
                () => jwt.ValidateAuth("invalidtoken"));

            var invalidTokenSplit = token.Split('.');
            invalidTokenSplit[2] = "SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            Assert.Throws<SignatureVerificationException>(
                () => jwt.ValidateAuth(string.Join('.', invalidTokenSplit)));

            Thread.Sleep(1050);
            Assert.Throws<TokenExpiredException>(
                () => jwt.ValidateAuth(token));
        }
    }
}
