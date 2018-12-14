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
        private static string _user { get; set; }
        public static bool UserStored { get; private set; }

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
            _user = username;
            var vault = new PasswordVault();
            vault.Add(new PasswordCredential(
                "CityApp", username, password));
            UserStored = true;
        }

        public static PasswordCredential GetUserCredentials()
        {
            var vault = new PasswordVault();
            return vault.Retrieve("CityApp", _user);
        }

        public static void RemoveUserCredentials()
        {
            var vault = new PasswordVault();
            vault.Remove(GetUserCredentials());
            UserStored = false;
        }
    }
}
