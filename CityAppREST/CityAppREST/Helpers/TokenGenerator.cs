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
        private readonly byte[] _key;

        public byte[] Key => _key;

        public TokenGenerator()
        {
            _key = new byte[256];
            new RNGCryptoServiceProvider().GetBytes(_key);
        }

        public string GenerateTokenForUser(User user)
        {
            var ssk = new SymmetricSecurityKey(_key);
            var creds = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(creds);
            var userData = new JwtPayload { { "username", user.Username }, { "userType", user.UserType } };

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(header, userData));
        }

    }
}
