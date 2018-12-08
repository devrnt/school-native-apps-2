using System;
using System.Security.Cryptography;

namespace CityAppREST.Helpers
{
    public static class PasswordHasher
    {
        private static readonly int _saltLength = 16;
        private static readonly int _hashLength = 20;
        private static readonly int _hashIterations = 1000;

        public static string GetPasswordAndSaltHash(string password)
        {
            var salt = new byte[_saltLength];
            new RNGCryptoServiceProvider().GetBytes(salt);

            var rfc = new Rfc2898DeriveBytes(password, salt, _hashIterations);
            var hash = rfc.GetBytes(_hashLength);
            var hashBytes = new byte[_saltLength + _hashLength];

            Array.Copy(salt, 0, hashBytes, 0, _saltLength);
            Array.Copy(hash, 0, hashBytes, _saltLength, _hashLength);
            return Convert.ToBase64String(hashBytes);
        }

        public static Boolean VerifyPasswordWithHash(string password, string passwordHash)
        {
            var hashBytes = Convert.FromBase64String(password);

            var salt = new byte[_saltLength];
            Array.Copy(hashBytes, salt, _saltLength);

            var rfc = new Rfc2898DeriveBytes(password, salt, _hashIterations);
            var hash = rfc.GetBytes(_hashLength);

            for (int i = 0; i < _hashLength; i++)
            {
                if (hashBytes[i + _saltLength] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
