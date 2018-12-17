using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Promotion> Promotions { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand AddPromotionCommand { get; set; }
        public RelayCommand AddDiscountCommand { get; set; }
        public RelayCommand DeletePromotionCommand { get; set; }
        private INavigationService _navigationService;
        private UserService _userService;

        public EditCompanyDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _userService = UserService.us;
            AddPromotionCommand = new RelayCommand((p) => AddPromotion((Company)p));
            AddDiscountCommand = new RelayCommand((p) => AddDiscount((Company)p));
            DeletePromotionCommand = new RelayCommand((p) => DeletePromotions());
        }

        private void AddDiscount(Company c)
        {
            NavigationService.ns.NavigateToAddDiscountAsync();
        }
        private void AddPromotion(Company c)
        {
            NavigationService.ns.NavigateToAddPromotionAsync();
        }
        private void DeletePromotions()
        {
            Promotions.Clear();
            Company.Promotions.Clear();
        }
        Task INavigableTo.NavigatedTo(NavigationMode navigationMode, object parameter)
        {
            if (navigationMode != NavigationMode.Back && parameter is Company company)
            {
                Company = company;
                AddPromotionCommand = new RelayCommand((p) => AddPromotion((Company)p));
                AddDiscountCommand = new RelayCommand((p) => AddDiscount((Company)p));
                Promotions = new ObservableCollection<Promotion>();
                foreach (Promotion p in company.Promotions)
                {
                    Promotions.Add(p);
                   
                }
            }
            return null;
        }
    }
}
