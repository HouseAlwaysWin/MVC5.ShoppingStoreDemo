using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using ShoppingStore.Domain.Infrastructure;
using System;
using System.IdentityModel.Tokens;
using Thinktecture.IdentityModel.Tokens;

namespace ShoppingStore.Infractructure.Providers
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string issuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            this.issuer = issuer;
        }
        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentException("data");
            }

            string audienceId = "e672f43a307d48978f5d60c51fc2944c";

            //var key = new byte[32];

            //RNGCryptoServiceProvider.Create().GetBytes(key);

            //var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key);

            //var signingKey = new Microsoft.IdentityModel.Tokens.SigningCredentials(
            //    securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            var key = "wzLHzvMqSK9t7R1Ctcf1Y54IpJhOOW3kKMYAtOHnW7o=";

            var keyByteArray = TextEncodings.Base64Url.Decode(key);

            var sigingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;

            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(
                issuer, audienceId, data.Identity.Claims,
                issued.Value.UtcDateTime, expires.Value.UtcDateTime, sigingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}