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
using CityApp.Services.Rest;
using Windows.UI.Xaml.Navigation;

namespace CityApp.ViewModels
{
    public class CompanyDetailsViewModel : INavigableTo, INotifyPropertyChanged
    {
        public Company Company { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand SubscribeCommand { get; set; }

        private INavigationService _navigationService;
        private UserService _userService;

        public CompanyDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _userService = new UserService();
            SubscribeCommand = new RelayCommand((_) => AddCompanyToSubscriptionAsync());
        }

        public CompanyDetailsViewModel()
        {
            _userService = new UserService();
            SubscribeCommand = new RelayCommand((_) => AddCompanyToSubscriptionAsync());
        }

        private async Task AddCompanyToSubscriptionAsync()
        {
            await _userService.AddCompanyToSubscription(Company);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Company)));
        }

        public void NavigatedTo(NavigationMode navigationMode, object parameter)
        {
            if (navigationMode != NavigationMode.Back && parameter is Company company)
            {
                Company = company;
            }
        }
    }
}
