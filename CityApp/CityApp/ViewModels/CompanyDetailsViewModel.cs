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

        public string SubscribeButtonText { get; set; } = "Standaard";

        private INavigationService _navigationService;
        private UserService _userService;

        public CompanyDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _userService = new UserService();
            CheckIfCompanyIsAlreadySubscribed();
            SubscribeCommand = new RelayCommand(async (_) => await AddCompanyToSubscriptionAsync());
        }

        private async Task<bool> CheckIfCompanyIsAlreadySubscribed()
        {
            // check if there is a user
            var token = StorageService.RetrieveUserId();
            if (StorageService.UserType == -1)
            {
                return false;
            }

            var user = await _userService.GetUser();
            var subscriptions = user.Subscriptions;

            if (subscriptions.Select(c => c.Id).Contains(Company.Id))
            {
                SubscribeButtonText = "Geabonneerd";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubscribeButtonText)));
                return false;
            }
            else
            {
                SubscribeButtonText = "Abonneer";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubscribeButtonText)));
                return true;
            }
        }

        private async Task AddCompanyToSubscriptionAsync()
        {
            await _userService.AddCompanyToSubscription(Company);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Company)));
            if (await CheckIfCompanyIsAlreadySubscribed())
            {
                AlertService.Toast($"Bedrijf {Company.Name}", $"Afgemeld op bedrijf {Company.Name}");

            }
            else
            {
                AlertService.Toast($"Bedrijf {Company.Name}", $"Geabonneerd op bedrijf {Company.Name}");

            }
        }

        Task INavigableTo.NavigatedTo(NavigationMode navigationMode, object parameter)
        {
            if (navigationMode != NavigationMode.Back && parameter is Company company)
            {
                Company = company;
            }
            return Task.FromResult<object>(null);
        }
    }
}
