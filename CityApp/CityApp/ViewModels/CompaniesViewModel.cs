using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CityApp.DataModel;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;
using System.Linq;
using CityApp.Services.Rest;

namespace CityApp.ViewModels
{
    public class CompaniesViewModel : INotifyPropertyChanged
    {
        #region === Fields ===
        private readonly INavigationService _navigationService;
        public ObservableCollection<Company> Companies { get; set; }
        public List<Categories> AllCategories { get; set; }

        public List<string> PromotionsChoices { get; set; }

        private readonly CompanyService _companyService;
        // add the list of companies here in future
        // IQueryable<Company> _companies;
        #endregion

        #region === Properties ===
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand CompanyDetailsCommand { get; set; }
        public RelayCommand FilterChangeCommand { get; set; }
        #endregion

        #region === Constructor ===
        public CompaniesViewModel()
        {
            _navigationService = NavigationService.ns;
            _companyService = new CompanyService();
            Companies = new ObservableCollection<Company>();
            CompanyDetailsCommand = new RelayCommand((p) => ShowCompanyDetails((Company)p));
            FilterChangeCommand = new RelayCommand((p) => UpdateFilter((Categories)p, false));
            LoadCompaniesAsync();
            AllCategories = new List<Categories>();
            PromotionsChoices = new List<string>() { "Ja", "Nee" };
            foreach (Categories cat in Enum.GetValues(typeof(Categories)))
            {
                AllCategories.Add(cat);
            }
        }
        public CompaniesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion

        #region === Methods ===
        public async void LoadCompaniesAsync()
        {
            var companies = await _companyService.GetCompanies();
            companies.ForEach(company => Companies.Add(company));
            // Companies = new ObservableCollection<Company>(companies);
        }

        // Imagine we have a button on the Companies.xaml with a redirection to another page
        // Then:
        // Task NavigateToWhateverPageAsync() {_navigatinService.NavigateToWhateverPage()}
        // Awesome isn't it?
        public async void ShowCompanyDetails(Company p)
        {
            var x = new CompanyService();
            var company = await x.GetCompany(p.Id);
            var c = (Company)company;
            await _navigationService.NavigateToCompanyDetailsAsync(c);
        }

        public ObservableCollection<Company> UpdateFilter(Categories categorie, bool shouldHavePromo)
        {
            var copyCompanies = Companies.ToList();

            if (categorie != Categories.All)
            {
                copyCompanies = copyCompanies.Where(c => c.Categorie == categorie).ToList();
            }

            if (shouldHavePromo)
            {
                copyCompanies = copyCompanies
                    .TakeWhile(c => c.Promotions != null)
                    .Where(c => c.Promotions.Count > 0)
                    .ToList();
            }
            return new ObservableCollection<Company>(copyCompanies);
        }
        public ObservableCollection<Company> ResetFilter()
        {
            return Companies;
        }
        #endregion
    }
}
