using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;

namespace CityApp.ViewModels
{
    class AccountViewModel : INotifyPropertyChanged
    {
        #region === Fields ===
        private INavigationService _navigationService;
        // add the list of companies here in future
        // IQueryable<Company> _companies;
        #endregion

        #region === Properties ===
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand LogInCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

        #endregion

        #region === Constructor ===
        public AccountViewModel()
        {
            LogInCommand = new RelayCommand((p) => LogIn((LogInCredentials)p));
            RegisterCommand = new RelayCommand((p) => Register());
            _navigationService = NavigationService.ns;
        }
        public AccountViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        #endregion

        #region === Methods ===
        // Imagine we have a button on the Companies.xaml with a redirection to another page
        // Then:
        // Task NavigateToWhateverPageAsync() {_navigatinService.NavigateToWhateverPage()}
        // Awesome isn't it?
        private void LogIn(LogInCredentials c) {
        }
        private void Register() {
            _navigationService.NavigateToRegisterAsync();
        }
        #endregion
    }
}
