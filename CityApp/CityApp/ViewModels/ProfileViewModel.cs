using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.Services;
using CityApp.Services.Rest;

namespace CityApp.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; private set; }
        public String test = "x";
        public ProfileViewModel()
        {
            LoadUser();
        }

        private async void LoadUser()
        {
            User = await UserService.us.GetUser(StorageService.GetUserCredentials().UserName);
        }
    }
}
