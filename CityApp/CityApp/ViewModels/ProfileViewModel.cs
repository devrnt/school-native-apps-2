using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.DataModel.Responses;
using CityApp.Services;
using CityApp.Services.Rest;

namespace CityApp.ViewModels
{
    public class ProfileViewModel
    {
        public UserResponse User { get; private set; }
        public String test = "x";
        public ProfileViewModel()
        {
            LoadUser();
        }

        private async void LoadUser()
        {
            User = await UserService.us.GetUser();
        }
    }
}
