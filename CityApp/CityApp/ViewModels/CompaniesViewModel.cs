using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CityApp.DataModel;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;

namespace CityApp.ViewModels
{
    public class CompaniesViewModel : INotifyPropertyChanged
    {
        #region === Fields ===
        private INavigationService _navigationService;
        public ObservableCollection<Company> Companies { get; set ; }
        // add the list of companies here in future
        // IQueryable<Company> _companies;
        #endregion

        #region === Properties ===
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand CompanyDetailsCommand { get; set; }

        #endregion

        #region === Constructor ===
        public CompaniesViewModel() {
            Companies = new ObservableCollection<Company>(DummyDataSource.Companies);
            CompanyDetailsCommand = new RelayCommand((p) => ShowCompanyDetails((Company)p));
            _navigationService = NavigationService.ns;
        }

        public CompaniesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        #endregion

        #region === Methods ===
        // Imagine we have a button on the Companies.xaml with a redirection to another page
        // Then:
        // Task NavigateToWhateverPageAsync() {_navigatinService.NavigateToWhateverPage()}
        // Awesome isn't it?
        private void ShowCompanyDetails(Company p)
        {
            _navigationService.NavigateToCompanyDetailsAsync(p);
        }
        
        #endregion
    }
}
