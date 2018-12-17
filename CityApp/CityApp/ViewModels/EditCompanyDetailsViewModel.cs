using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.DataModel.CommandParameters;
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
        public ObservableCollection<Discount> Discounts { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RelayCommand DeletePromotionCommand { get; set; }
        public RelayCommand DeleteDiscountCommand { get; set; }

        private INavigationService _navigationService;
        private UserService _userService;




        public EditCompanyDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _userService = UserService.us;
            DeletePromotionCommand = new RelayCommand((p) => DeletePromotions());
            DeleteDiscountCommand = new RelayCommand((p) => DeleteDiscounts());
        }
        public void AddPromotion(String s, Object d) {
            Promotion p = new Promotion(s, (Discount)d);
            Promotions.Add(p);
            Company.Promotions.Add(p);
        }
        public void AddDiscount(string c, string pdf) {
            Discount d = new Discount(c, pdf);
            Discounts.Add(d);
            Company.Discounts.Add(d);
        }
        private void DeletePromotions()
        {
            Promotions.Clear();
            Company.Promotions.Clear();
        }
        private void DeleteDiscounts()
        {
            Discounts.Clear();
            Company.Discounts.Clear();
        }
        Task INavigableTo.NavigatedTo(NavigationMode navigationMode, object parameter)
        {
            if (navigationMode != NavigationMode.Back && parameter is Company company)
            {
                Company = company;
                Promotions = new ObservableCollection<Promotion>();
                foreach (Promotion p in company.Promotions)
                {
                    Promotions.Add(p);
                }
                Discounts = new ObservableCollection<Discount>();
                foreach (Discount d in company.Discounts)
                {
                    Discounts.Add(d);
                }
            }
            return Task.FromResult<object>(null);
        }
    }
}
