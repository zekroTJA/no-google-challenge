using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using ToDoList.Util;

namespace ToDoList.Modules
{
    public class JWTAuthorization : IAuthorization
    {
        private readonly byte[] signingKey;
        private readonly JwtBuilder builder;

        public JWTAuthorization(byte[] _signingKey)
        {
            signingKey = _signingKey;
            builder = new JwtBuilder()
                .WithSecret(signingKey)
                .WithAlgorithm(new HMACSHA256Algorithm());
        }

        public JWTAuthorization() : this(CryptoRandom.GetBytes(256)) 
        { }

        public string GetAuthToken(AuthClaims claims) =>
            builder.AddClaim("sub", claims.SessionId)
                   .AddClaim("uid", claims.UserId)
                   .IssuedAt(DateTime.Now)
                   .Encode();

        public AuthClaims ValidateAuth(string token) =>
            new AuthClaims(
                builder.MustVerifySignature()
                       .Decode<IDictionary<string, object>>(token));
    }
}
