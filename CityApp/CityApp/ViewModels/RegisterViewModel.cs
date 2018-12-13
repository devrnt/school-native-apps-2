using System;
using System.Collections.Generic;
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
    public class RegisterViewModel
    {
        private INavigationService _navigationService;
        private UserService _userService;
        public RelayCommand RegisterCommand { get; set; }


        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand((p) => RegisterAsync((RegisterCredentials)p));
            _navigationService = NavigationService.ns;
            _userService = UserService.us;
        }

        private async void RegisterAsync(RegisterCredentials p)
        {
            UserType ut;
            if (p.Type != null || p.Type == true)
            {
                ut = UserType.Visitor;
            }
            else
            {
                ut = UserType.Owner;
            }

            var user = new Visitor(p.Name, p.FirstName, p.BirthDate.DateTime, p.Email, p.Password, ut, new List<Company>());

            var result = await _userService.RegisterAsync(user);
            if(result == "Succesvol aangemaakt")
            {
                await _navigationService.NavigateToCompaniesAsync();
            } else
            {
                // Not logged in, display error message
            }
        }
    }
}
