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
        private CompaniesViewModel _cv;

        public Companies()
        {
            this.InitializeComponent();
            _cv = new CompaniesViewModel();
            this.DataContext = _cv;
        }

        private void fCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Categories cat = fCat.SelectedItem == null ? Categories.All : (Categories)fCat.SelectedItem;
            gv.ItemsSource = _cv.UpdateFilter(cat);
        }

        private void Reset_Filters(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            fCat.SelectedIndex = -1;
            gv.ItemsSource = _cv.ResetFilter();
        }
    }
}
