using CityApp.DataModel;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;
using CityApp.ViewModels;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CityApp.Views
{
    public sealed partial class Companies : Page, IPageWithViewModel<CompaniesViewModel>
    {
        private CompaniesViewModel _cv;
        public CompaniesViewModel ViewModel { get; set; }
        private Categories cat { get { return fCat.SelectedItem == null ? Categories.All : (Categories)fCat.SelectedItem; } }
        private bool promo { get { return fPromo.IsChecked == null ? false : fPromo.IsChecked == false ? false : true; } }

        public Companies()
        {
            this.InitializeComponent();
            _cv = new CompaniesViewModel();
            this.DataContext = _cv;
        }

        private void fCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Categories cat = fCat.SelectedItem == null ? Categories.All : (Categories)fCat.SelectedItem;
            gv.ItemsSource = _cv.UpdateFilter(cat, promo);
        }

        private void Reset_Filters(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            fCat.SelectedIndex = -1;
            fPromo.IsChecked = false;
            gv.ItemsSource = _cv.ResetFilter();
        }
        private void Update_Filters(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            gv.ItemsSource = _cv.UpdateFilter(cat, promo);
        }

        private void Search_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // TODO: Fetch all the available companies from a service and get them here in the list
                var companies = _cv.Companies;
                var results = companies.Where(c => c.Name.IndexOf(sender.Text, System.StringComparison.CurrentCultureIgnoreCase) >= 0 
                || c.Categorie.ToString().IndexOf(sender.Text, System.StringComparison.CurrentCultureIgnoreCase) >= 0);
                sender.ItemsSource = results.ToList();
            }
        }

        private void Search_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            ViewModel.ShowCompanyDetails(args.SelectedItem as CityApp.DataModel.Company);
        }
    }
}
