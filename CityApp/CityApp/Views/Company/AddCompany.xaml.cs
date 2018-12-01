using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CityApp.DataModel;
using CityApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CityApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddCompany : Page
    {
        private AddCompanyViewModel _vm;

        public AddCompany()
        {
            this.InitializeComponent();
            this._vm = new AddCompanyViewModel();
            this.DataContext = _vm;
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreateCompany(object sender, RoutedEventArgs e)
        {
            Owner owner = null;
            List<Location> locations = new List<Location>();
            locations.Add(new Location("Belgie", Input_CompanyCity.Text, int.Parse(Input_CompanyPostal.Text), Input_CompanyStreet.Text, int.Parse(Input_CompanyNumber.Text)));
            
            
            OpeningHours openingHours = new OpeningHours(getDagen(), ou_van.Text, ou_tot.Text);
            string leaveOfAbsence = null;
            SocialMedia socialMedia = new SocialMedia(cb_Facebook.IsChecked == null ? null : cb_Facebook.IsChecked == false ? null : sm_Facebook.Text,
                cb_Twitter.IsChecked == null ? null : cb_Twitter.IsChecked == false ? null : sm_Twitter.Text,
                cb_Youtube.IsChecked == null ? null : cb_Youtube.IsChecked == false ? null : sm_Youtube.Text,
                cb_Google.IsChecked == null ? null : cb_Google.IsChecked == false ? null : sm_Google.Text);
            this._vm.CreateCompany(Input_CompanyName.Text, Input_CompanyDescription.Text, Input_CompanyKeywords.Text, (Categories)Input_CategoryComboBox.SelectedItem, owner, locations, openingHours, leaveOfAbsence, socialMedia);
        }

        private List<Days> getDagen()
        {
            List<Days> dagen = new List<Days>();
            if (ou_maandag.IsChecked == null ? false : ou_maandag.IsChecked == false ? false : true) {
                dagen.Add(Days.Maandag);
            }
            if (ou_dinsdag.IsChecked == null ? false : ou_dinsdag.IsChecked == false ? false : true)
            {
                dagen.Add(Days.Dinsdag);
            }
            if (ou_woensdag.IsChecked == null ? false : ou_woensdag.IsChecked == false ? false : true)
            {
                dagen.Add(Days.Woensdag);
            }
            if (ou_donderdag.IsChecked == null ? false : ou_donderdag.IsChecked == false ? false : true)
            {
                dagen.Add(Days.Donderdag);
            }
            if (ou_vrijdag.IsChecked == null ? false : ou_vrijdag.IsChecked == false ? false : true)
            {
                dagen.Add(Days.Vrijdag);
            }
            if (ou_zaterdag.IsChecked == null ? false : ou_zaterdag.IsChecked == false ? false : true)
            {
                dagen.Add(Days.Zaterdag);
            }
            if (ou_zondag.IsChecked == null ? false : ou_zondag.IsChecked == false ? false : true)
            {
                dagen.Add(Days.Zondag);
            }            
            return dagen;
        }
    }
}
