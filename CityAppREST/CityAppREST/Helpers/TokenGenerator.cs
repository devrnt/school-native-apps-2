using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CityAppREST.Models;
using Microsoft.IdentityModel.Tokens;

namespace CityAppREST.Helpers
{
    /// <summary>
    /// Helper class to generate a JWT token with Claims
    /// </summary>
    public class TokenGenerator
    {
        private readonly byte[] _key;

        public byte[] Key => _key;

        public TokenGenerator()
        {
            _key = new byte[256];
            new RNGCryptoServiceProvider().GetBytes(_key); // key is randomly generated to keep secret, can also be hardcoded in a json file
        }

        public string GenerateTokenForUser(User user)
        {
            // Create security key and signs credentials with it
            var ssk = new SymmetricSecurityKey(_key);
            var creds = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256Signature);

            // Claims containing data about the user
            var userClaims = new List<Claim> {
               new Claim(ClaimTypes.Name, user.Username),
               new Claim(ClaimTypes.Role, user.UserType.ToString()) // will be validated against possible policies
            };

            // Adds all roles if user is Admin
            if (user.UserType == UserType.Admin)
            {
                foreach (var type in Enum.GetNames(typeof(UserType)))
                {
                    if (type != "Admin")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, type));
                    }
                }
            }

            var token = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds // add credentials so token can be validated upon later authentication
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
