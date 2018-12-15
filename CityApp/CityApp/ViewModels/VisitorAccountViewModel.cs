using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;
using CityApp.Services.Rest;

namespace CityApp.ViewModels
{
    public class VisitorAccountViewModel
    {
        public RelayCommand LogOutCommand { get; set; }

        public VisitorAccountViewModel()
        {
            LogOutCommand = new RelayCommand((p) => ClearStoredUser());
        }
        private async void ClearStoredUser()
        {
            await UserService.LogOutUserAsync();
            await NavigationService.ns.NavigateToAccountAsync();
        }
    }
}
