using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.Services;
using CityApp.Services.Navigation;
using CityApp.Services.Rest;

namespace CityApp.ViewModels
{

    public class AddCompanyViewModel
    {
        private CompanyService _companyService;
        private NavigationService _navigationService;
        public List<Categories> AllCategories
        {
            get
            {
                List<Categories> cats = new List<Categories>();
                foreach (Categories cat in Enum.GetValues(typeof(Categories)))
                {
                    cats.Add(cat);
                }
                return cats;
            }
        }
        public AddCompanyViewModel()
        {
            _companyService = new CompanyService();
            _navigationService = NavigationService.ns;
        }

        public async void CreateCompany(string name, string description, string keyWordsString, Categories categorie, Owner owner, ICollection<Location> locations, IEnumerable<OpeningHours> openingHours, string leaveOfAbsence, SocialMedia socialMedia)
        {
            // is overwritten in the backend
            List<Promotion> promotions = null;
            var company = new Company(name, description, keyWordsString, categorie, owner, locations, openingHours, null, socialMedia, promotions);

            var responseCompany = await _companyService.AddCompany(company);
            Console.WriteLine(responseCompany);
            AlertService.Toast($"{responseCompany.Name} aangemaakt", $"{responseCompany.Name} succesvol toegevoegd");
            await _navigationService.NavigateToCompaniesAsync();
        }
    }
}
