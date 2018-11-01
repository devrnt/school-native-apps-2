using CityApp.DataModel;
using CityApp.Services;
using CityApp.Services.Navigation;
using CityApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CityApp.Views
{
    public sealed partial class Companies : Page
    {
        private INavigationService _navigationService;
        public Companies()
        {
            this.InitializeComponent();
        }
    }
}
