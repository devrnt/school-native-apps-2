using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            var token = new JwtSecurityToken(
                claims: new[] {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.UserType.ToString())
                },
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
