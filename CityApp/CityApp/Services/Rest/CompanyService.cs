using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CityApp.DataModel;
using Newtonsoft.Json;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace CityApp.Services.Rest
{
    public class CompanyService
    {
        // Used for a workaround for the ssl certificate
        private readonly HttpBaseProtocolFilter _httpFilter;
        private readonly HttpClient _httpClient;

        // REST endpoint url
        private const string _apiUrl = "https://localhost:44321/api/companies";
        // private const string _apiUrl = "https://cityapprest.azurewebsites.net/api/companies";

        public CompanyService()
        {
            _httpFilter = new HttpBaseProtocolFilter();
            _httpFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Expired);
            _httpFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            _httpFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);

            _httpClient = new HttpClient(_httpFilter);
        }

        public async Task<List<Company>> GetCompanies()
        {
            var json = await _httpClient.GetStringAsync(new Uri(_apiUrl));

            return JsonConvert.DeserializeObject<List<Company>>(json);
        }

        public async Task<Company> GetCompany(int id)
        {
            var json = await _httpClient.GetStringAsync(new Uri($"{_apiUrl}/{id}"));
            return JsonConvert.DeserializeObject<Company>(json);
        }

        public async Task<Company> AddCompany(Company company)
        {
            var token = await StorageService.RetrieveUserToken();
            var userId = await StorageService.RetrieveUserId();

            var copy = company.Owner.Id = int.Parse(userId);

            var companyJson = JsonConvert.SerializeObject(company);
            var companyPostReady = new HttpStringContent(companyJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.PostAsync(new Uri(_apiUrl), companyPostReady);

            return JsonConvert.DeserializeObject<Company>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Promotion> AddPromotion(int companyId, Promotion promotion)
        {
            var token = await StorageService.RetrieveUserToken();

            var promotionJson = JsonConvert.SerializeObject(promotion);
            var promotionPostReady = new HttpStringContent(promotionJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.PostAsync(new Uri($"{_apiUrl}/{companyId}/promotions"), promotionPostReady);

            return JsonConvert.DeserializeObject<Promotion>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Event> AddEvent(int companyId, Event @event)
        {
            var token = await StorageService.RetrieveUserToken();

            var eventJson = JsonConvert.SerializeObject(@event);
            var eventPostReady = new HttpStringContent(eventJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.PostAsync(new Uri($"{_apiUrl}/{companyId}/events"), eventPostReady);

            return JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Company> RemoveCompany(int companyId)
        {
            var token = await StorageService.RetrieveUserToken();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.DeleteAsync(new Uri($"{_apiUrl}/{companyId}"));

            return JsonConvert.DeserializeObject<Company>(await response.Content.ReadAsStringAsync());
        }
    }
}
