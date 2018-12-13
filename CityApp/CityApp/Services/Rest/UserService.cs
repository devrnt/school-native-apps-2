﻿using System;
using System.Threading.Tasks;
using CityApp.DataModel;
using Newtonsoft.Json;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace CityApp.Services.Rest
{
    public class UserService
    {
        private static UserService _us;
        public static UserService us
        {
            get
            {
                if (_us == null) { _us = new UserService(); }
                return _us;
            }
            set { _us = value; }
        }
        public int LoggedUser { get; internal set; }


        // Used for a workaround for the ssl certificate
        private readonly HttpBaseProtocolFilter _httpFilter;
        private readonly HttpClient _httpClient;

        // REST endpoint url
        private const string _apiUrl = "https://localhost:44321/api/users";

        public UserService()
        {
            _httpFilter = new HttpBaseProtocolFilter();
            _httpFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Expired);
            _httpFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            _httpFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);

            _httpClient = new HttpClient(_httpFilter);
        }

        public async Task<string> RegisterAsync(User user)
        {
            var userJson = JsonConvert.SerializeObject(user);
            var userPostReady = new HttpStringContent(userJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

            var response = await _httpClient.PostAsync(new Uri(_apiUrl), null);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content);
                return "Succesvol aangemaakt";
            }
            else
            {
                //    return response;
            }
            return response.Content.ToString();
        }
        public async Task<string> AuthenticateAsync(LogInCredentials user)
        {
            var userJson = JsonConvert.SerializeObject(user);
            var userPostReady = new HttpStringContent(userJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

            var response = await _httpClient.PostAsync(new Uri(_apiUrl + "/authenticate"), userPostReady);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(await response.Content.ReadAsStringAsync());
                await StorageService.StoreUserToken(loginResponse.Token);

                return "Succesvol ingelogd";
            }
            else
            {
                return response.Content.ToString();
            }
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
