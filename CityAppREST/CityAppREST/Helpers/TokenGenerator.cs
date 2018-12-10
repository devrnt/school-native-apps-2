using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using CityAppREST.Models;
using Microsoft.IdentityModel.Tokens;

namespace CityAppREST.Helpers
{
    public class TokenGenerator
    {
        private readonly string _key;

        public TokenGenerator()
        {
            var keyBytes = new byte[256];
            new RNGCryptoServiceProvider().GetBytes(keyBytes);

            _key = Convert.ToBase64String(keyBytes);
        }

        public string GenerateTokenForUser(User user)
        {
            var ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(creds);
            var userData = new JwtPayload { { "username", user.Username } };

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(header, userData));
        }
    }
}
