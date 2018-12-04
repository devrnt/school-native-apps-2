using System.Collections.Generic;
using System.Collections.ObjectModel;
using CityApp.DataModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CityApp.Views.Company
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPromotion : Page
    {
        // company passed by the previous page to add the promotion
        public CityApp.DataModel.Company Company { get; set; }
        public ObservableCollection<Discount> Discounts { get; set; }

        public AddPromotion()
        {
            // Tijdelijk, zal altijd een valid company zijn
            Company = DummyDataSource.Companies[0];
            Company.Discounts = new List<Discount>() { new Discount("couponcode") };
            Discounts = new ObservableCollection<Discount>(Company.Discounts);
            this.InitializeComponent();
        }

        private void CreatePromotion_Click(object sender, RoutedEventArgs e)
        {
            var selectedDiscount = Discounts_ComboBox.SelectedItem as Discount;
            var promotion = new Promotion(Input_Omschrijving.Text, selectedDiscount);

            Company.Promotions.Add(promotion);
            // post the new company to the REST api

        }
    }
}
