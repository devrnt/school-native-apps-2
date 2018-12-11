using System;
using System.Security.Cryptography;

namespace CityAppREST.Helpers
{
    /// <summary>
    /// Helper class to hash a password and validate a password with a passwordHash
    /// </summary>
    public static class PasswordHasher
    {
        private static readonly int _saltLength = 16;
        private static readonly int _hashLength = 20;
        private static readonly int _hashIterations = 1000; // increasing this makes the hash more secure but impacts performance

        public static string GetPasswordAndSaltHash(string password)
        {
            // Generate a random salt
            var salt = new byte[_saltLength];
            new RNGCryptoServiceProvider().GetBytes(salt);

            // Hashes the password
            var rfc = new Rfc2898DeriveBytes(password, salt, _hashIterations);
            var hash = rfc.GetBytes(_hashLength);

            // Adds password hash and salt in one byte array
            var hashBytes = new byte[_saltLength + _hashLength];
            Array.Copy(salt, 0, hashBytes, 0, _saltLength);
            Array.Copy(hash, 0, hashBytes, _saltLength, _hashLength);

            return Convert.ToBase64String(hashBytes);
        }

        public static Boolean VerifyPasswordWithHash(string password, string passwordHash)
        {
            var hashBytes = Convert.FromBase64String(passwordHash);

            // Get the salt from the hashBytes
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
