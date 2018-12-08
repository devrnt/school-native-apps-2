using System;
using System.Security.Cryptography;

namespace CityAppREST.Helpers
{
    public static class PasswordHasher
    {
        public static string GetPasswordAndSaltHash(string password)
        {
            var salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            var rfc = new Rfc2898DeriveBytes(password, salt, 1000);
            var hash = rfc.GetBytes(20);
            var hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public static Boolean VerifyPasswordWithHash(string password, string hash)
        {
            return false;
        }
    }
}
