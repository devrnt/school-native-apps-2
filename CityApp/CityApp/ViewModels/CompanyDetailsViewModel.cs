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
using Windows.UI.Xaml.Navigation;

namespace CityApp.ViewModels
{
    public class CompanyDetailsViewModel : INavigableTo, INotifyPropertyChanged
    {
        public Company Company { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand SubscribeCommand { get; set; }

        private INavigationService _navigationService;

        public CompanyDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SubscribeCommand = new RelayCommand((p) => AddCompanyToSubscription());
        }

        private void AddCompanyToSubscription()
        {
            Console.WriteLine(Company);
        }

        public void NavigatedTo(NavigationMode navigationMode, object parameter)
        {
            if (navigationMode != NavigationMode.Back && parameter is Company company)
            {
                Company = company;
            }
        }
    }
}
