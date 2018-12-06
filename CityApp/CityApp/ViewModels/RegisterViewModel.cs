using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;

namespace CityApp.ViewModels
{
    public class RegisterViewModel
    {
        private INavigationService _navigationService;
        public RelayCommand RegisterCommand { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand((p) => Register((RegisterCredentials)p));
            _navigationService = NavigationService.ns;
        }

        private void Register(RegisterCredentials p)
        {
            UserType ut;
            if (p.Type != null || p.Type == true) {
                ut = UserType.Visitor;
            }else
            {
                ut = UserType.Owner;
            }


            User user = new Visitor(p.Name, p.FirstName, p.BirthDate.DateTime, p.Email, p.Password, ut, new List<Company>());
        }
    }
}
