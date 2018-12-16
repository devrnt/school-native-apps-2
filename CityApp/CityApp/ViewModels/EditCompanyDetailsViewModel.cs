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
        public RelayCommand AddDiscountCommand { get; set; }

        private INavigationService _navigationService;
        private UserService _userService;

        public EditCompanyDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _userService = UserService.us;
            AddPromotionCommand = new RelayCommand((p) => AddPromotion((Company)p));
            AddDiscountCommand = new RelayCommand((p) => AddDiscount((Company)p));
        }

        private void AddDiscount(Company c)
        {
            NavigationService.ns.NavigateToAddDiscountAsync();
        }

        private void AddPromotion(Company c)
        {
            NavigationService.ns.NavigateToAddPromotionAsync();
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
