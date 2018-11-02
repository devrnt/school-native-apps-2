using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.Services;
using CityApp.Services.Navigation;
using Windows.UI.Xaml.Navigation;

namespace CityApp.ViewModels
{
    public class CompanyDetailsViewModel : INavigableTo, INotifyPropertyChanged
    {
        public Company Company { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private INavigationService _navigationService;

        public CompanyDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public async Task NavigatedTo(NavigationMode navigationMode, object parameter)
        {
            if (navigationMode != NavigationMode.Back && parameter is Company company)
            {
                Company = company;
            }
        }
    }
}
