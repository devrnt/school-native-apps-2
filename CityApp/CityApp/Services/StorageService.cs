using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.Helpers;
using Windows.Security.Credentials;
using Windows.Storage;

namespace CityApp.Services
{
    public static class StorageService
    {
        private const string _tokenKey = "UserToken";

        // === User token ===
        public static async Task StoreUserToken(string token)
        {
            await ApplicationData
                .Current
                .LocalSettings
                .SaveAsync(_tokenKey, token);
        }

        public static async Task<string> RetrieveUserToken()
        {
            return await ApplicationData
                .Current
                .LocalSettings
                .ReadAsync<string>(_tokenKey);
        }

        // === User credentials: username and password ===
        public static void StoreUserCredentials(string username, string password)
        {
            var vault = new PasswordVault();
            vault.Add(new PasswordCredential(
                "CityApp", username, password));
        }

        public static PasswordCredential GetUserCredentials(string username)
        {
            var vault = new PasswordVault();
            return vault.Retrieve("CityApp", username);
        }

        public static void RemoveUserCredentials(string username, string password)
        {
            var vault = new PasswordVault();
            vault.Remove(new PasswordCredential(
                "CityApp", username, password));
        }
    }
}
