using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;
using CityApp.Services.Rest;

namespace CityApp.ViewModels
{
    public class VisitorAccountViewModel
    {
        private NavigationService _navigationService;
        private UserService _userService;
        public RelayCommand LogOutCommand { get; set; }
        public RelayCommand CompanyDetailsCommand { get; set; }
        public ObservableCollection<Company> Companies { get; set; }


        public VisitorAccountViewModel()
        {
            Companies = new ObservableCollection<Company>();
            _navigationService = NavigationService.ns;
            _userService = new UserService();
            LogOutCommand = new RelayCommand((p) => ClearStoredUser());
            CompanyDetailsCommand = new RelayCommand((p) => ShowCompanyDetails((Company)p));
            LoadSubscriptions();
        }

        public async void ShowCompanyDetails(Company p)
        {
            var x = new CompanyService();
            var c = await x.GetCompany(p.Id);
            await _navigationService.NavigateToCompanyDetailsAsync(c);
        }

        private async void ClearStoredUser()
        {
            await UserService.LogOutUserAsync();
            await NavigationService.ns.NavigateToAccountAsync();
        }

        public async void LoadSubscriptions()
        {
            var user = await _userService.GetUser();
            var subscriptions = user.Subscriptions;
            subscriptions.ForEach(company => Companies.Add(company));
            // Companies = new ObservableCollection<Company>(companies);
        }
        
    }
}
