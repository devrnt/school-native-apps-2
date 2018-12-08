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
            var json = await _httpClient.GetStringAsync(new Uri(_apiUrl));

            return JsonConvert.DeserializeObject<Company>(json);
        }
    }
}
