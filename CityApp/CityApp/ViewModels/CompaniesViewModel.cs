using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CityApp.DataModel;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;
using System.Linq;

namespace CityApp.ViewModels
{
    public class CompaniesViewModel : INotifyPropertyChanged
    {
        #region === Fields ===
        private INavigationService _navigationService;
        public ObservableCollection<Company> Companies { get; set; }
        public List<Categories> AllCategories { get; set; }
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
            Companies = new ObservableCollection<Company>(DummyDataSource.Companies);
            AllCategories = new List<Categories>();
            foreach (Categories cat in Enum.GetValues(typeof(Categories)))
            {
                AllCategories.Add(cat);
            }
            CompanyDetailsCommand = new RelayCommand((p) => ShowCompanyDetails((Company)p));
            FilterChangeCommand = new RelayCommand((p) => UpdateFilter((Categories)p));

            _navigationService = NavigationService.ns;
        }
        public CompaniesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        internal void ShowCompanyDetails()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region === Methods ===
        // Imagine we have a button on the Companies.xaml with a redirection to another page
        // Then:
        // Task NavigateToWhateverPageAsync() {_navigatinService.NavigateToWhateverPage()}
        // Awesome isn't it?
        public void ShowCompanyDetails(Company p)
        {
            _navigationService.NavigateToCompanyDetailsAsync(p);
        }

        public ObservableCollection<Company> UpdateFilter(Categories cat)
        {
            List<Company> fCompanies = DummyDataSource.Companies;
            if (cat != Categories.All) {
                fCompanies = fCompanies.Where(p => p.Categorie == cat).ToList<Company>();
            }
          return new ObservableCollection<Company>(fCompanies);
        }
        public ObservableCollection<Company> ResetFilter()
        {
            List<Company> fCompanies = DummyDataSource.Companies;
            return new ObservableCollection<Company>(fCompanies);
        }
        #endregion
    }
}
