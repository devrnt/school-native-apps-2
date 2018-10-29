using CityApp.DataModel;
using CityApp.Services.Navigation;
using CityApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CityApp.Views
{
    public sealed partial class Companies : Page, IPageWithViewModel<CompaniesViewModel>
    {
        public CompaniesViewModel ViewModel { get; set; }

        public Companies()
        {
            this.InitializeComponent();
        }

        private void CompaniesGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedCompany = e.ClickedItem as Company;
            // navigate to details screen
            // ViewModel.NavigateToCompanyDetails(selectedCompany);
        }
    }
}
