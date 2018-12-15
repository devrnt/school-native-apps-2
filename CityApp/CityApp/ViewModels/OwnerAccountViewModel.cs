using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.DataModel.Responses;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;
using CityApp.Services.Rest;

namespace CityApp.ViewModels
{
    public class OwnerAccountViewModel
    {
        public UserResponse User { get; private set; }
        public String test = "x";
        public RelayCommand LogOutCommand { get; set; }
        public ObservableCollection<Company> Companies { get; set; }

        public OwnerAccountViewModel()
        {
            LogOutCommand = new RelayCommand((p) => ClearStoredUser());
            Companies = new ObservableCollection<Company>();
            LoadUser();
        }
        private async void ClearStoredUser()
        {
            await UserService.LogOutUserAsync();
            await NavigationService.ns.NavigateToAccountAsync();
        }
        private async void LoadUser()
        {
            var user = await UserService.us.GetUser();
            User = user;
            foreach (Company c in User.Companies) {
                Companies.Add(c);
            }
        }
    }
}
