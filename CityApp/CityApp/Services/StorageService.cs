using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.Helpers;
using Windows.Security.Credentials;
using Windows.Storage;

namespace CityApp.Services
{
    public static class StorageService
    {
        private const string _tokenKey = "UserToken";
        private const string _userIdKey = "UserId";
        private static string _user { get; set; }
        public static int UserType { get; set; } = -1;
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

        public static async Task StoreUserId(int id)
        {
            await ApplicationData
                .Current
                .LocalSettings
                .SaveAsync(_userIdKey, id);
        }

        public static async Task<string> RetrieveUserId()
        {
            return await ApplicationData
                .Current
                .LocalSettings
                .ReadAsync<string>(_userIdKey);
        }

        public static async Task ClearStoredUser()
        {
            await ApplicationData
            .Current
            .LocalSettings
            .SaveAsync(_userIdKey, "");
            await ApplicationData
            .Current
            .LocalSettings
            .SaveAsync(_tokenKey, "");

        }
        // === User credentials: username and password ===
        public static void StoreUserCredentials(string username, string password)
        {
            _user = username;
            var vault = new PasswordVault();
            vault.Add(new PasswordCredential(
                "CityApp", username, password));
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
        }
    }
}
