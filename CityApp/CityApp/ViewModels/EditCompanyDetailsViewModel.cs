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
    public class EditCompanyDetailsViewModel : INavigableTo, INotifyPropertyChanged
    {
        public Company Company { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand AddPromotionCommand { get; set; }

        private INavigationService _navigationService;
        private UserService _userService;

        public EditCompanyDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _userService = UserService.us;
            AddPromotionCommand = new RelayCommand((_) => AddPromotion());
        }

        public EditCompanyDetailsViewModel()
        {
            _userService = new UserService();
            AddPromotionCommand = new RelayCommand((_) => AddPromotion());
        }

        private Task AddPromotion()
        {
            throw new NotImplementedException();
        }

        Task INavigableTo.NavigatedTo(NavigationMode navigationMode, object parameter)
        {
            if (navigationMode != NavigationMode.Back && parameter is Company company)
            {
                Company = company;
            }
            return null;
        }
    }
}
