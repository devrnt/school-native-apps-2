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


        public RegisterViewModel()
        {
            _navigationService = NavigationService.ns;
            _userService = UserService.us;
        }

        public async void RegisterAsync(string sname, string fname, DateTimeOffset date, string email, string uname, string pass, bool ch)
        {
            User user;
            if (ch == true)
            {
                user = new Visitor(sname, fname, date.DateTime, email, pass, UserType.Visitor, new List<Company>());
            }
            else
            {
                user = new Owner(sname, fname, date.DateTime, email, pass, UserType.Owner, new List<Company>());
            }
            var result = await _userService.RegisterUser(user);
            AlertService.Toast($"Succesvol geregistreerd", $"Je bent nu ingelogd als {user.Username}");
            await _navigationService.NavigateToCompaniesAsync();
        }
    }
}
