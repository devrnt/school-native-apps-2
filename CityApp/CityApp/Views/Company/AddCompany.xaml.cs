using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CityApp.DataModel;
using CityApp.Services;
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
        public string CompanyNameError { get; set; }
        public string CompanyDescriptionError { get; set; }
        public string CompanyKeywordsError { get; set; }
        public string CompanyCategoryError { get; set; }
        public string CompanyCityError { get; set; }
        public string CompanyPostalError { get; set; }
        public string CompanyStreetError { get; set; }
        public string CompanyNumberError { get; set; }
        public string CompanyOpeningHoursError { get; set; }

        private bool mau;
        public bool Mau
        {
            get { return mau; }
            set { mau = value; Bindings.Update(); }
        }
        private bool diu;
        public bool Diu
        {
            get { return diu; }
            set { diu = value;
                Bindings.Update();
            }
        }
        private bool wou;
        public bool Wou
        {
            get { return wou; }
            set
            {
                wou = value;
                Bindings.Update();
            }
        }
        private bool dou;
        public bool Dou
        {
            get { return dou; }
            set
            {
                dou = value;
                Bindings.Update();
            }
        }
        private bool vru;
        public bool Vru
        {
            get { return vru; }
            set
            {
                vru = value;
                Bindings.Update();
            }
        }
        private bool zau;
        public bool Zau
        {
            get { return zau; }
            set
            {
                zau = value;
                Bindings.Update();
            }
        }
        private bool zou;
        public bool Zou
        {
            get { return zou; }
            set
            {
                zou = value;
                Bindings.Update();
            }
        }


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
            ValidateForm();
            if (!IsFormValid())
            {
                return;
            }

            var owner = new Owner(StorageService.RetrieveUserId());
            var locations = new List<Location>
            {
                new Location("Belgie", Input_CompanyCity.Text, int.Parse(Input_CompanyPostal.Text), Input_CompanyStreet.Text, int.Parse(Input_CompanyNumber.Text))
            };

            var openingHours = GetOpeningHours();

            var socialMedia = new SocialMedia(cb_Facebook.IsChecked == null ? null : cb_Facebook.IsChecked == false ? null : sm_Facebook.Text,
                cb_Twitter.IsChecked == null ? null : cb_Twitter.IsChecked == false ? null : sm_Twitter.Text,
                cb_Youtube.IsChecked == null ? null : cb_Youtube.IsChecked == false ? null : sm_Youtube.Text,
                cb_Google.IsChecked == null ? null : cb_Google.IsChecked == false ? null : sm_Google.Text);

            var company = new DataModel.Company()
            {
                Name = Input_CompanyName.Text,
                Description = Input_CompanyDescription.Text,
                KeyWords = Input_CompanyKeywords.Text,
                Categorie = (Categories)Input_CategoryComboBox.SelectedItem,
                Owner = owner,
                Locations = locations,
                OpeningHours = openingHours,
                //LeaveOfAbsence = leaveOfAbsence,
                SocialMedia = socialMedia
            };

            this._vm.CreateCompany(company.Name, company.Description, company.KeyWords, company.Categorie, company.Owner, company.Locations, company.OpeningHours.ToList(), "", company.SocialMedia);
        }

        private bool IsFormValid()
        {
            var errorList = new List<string>()
            {
                CompanyNameError,
                CompanyDescriptionError ,
                CompanyKeywordsError,
                CompanyCategoryError,
                CompanyCityError ,
                CompanyPostalError,
                CompanyStreetError,
                CompanyNumberError,
                CompanyOpeningHoursError
            };
            return errorList.All(error => error.Length == 0);
        }

        private void ValidateForm()
        {
            if (Input_CompanyName.Text.Length == 0)
            {
                CompanyNameError = "Naam mag niet leeg zijn";
            }
            else
            {
                CompanyNameError = "";
            }
            if (Input_CompanyDescription.Text.Length == 0)
            {
                CompanyDescriptionError = "Omschrijving mag niet leeg zijn";
            }
            else
            {
                CompanyDescriptionError = "";

            }
            if (Input_CompanyKeywords.Text.Length == 0)
            {
                CompanyKeywordsError = "Geef ten minste 1 zoekword op";
            }
            else
            {
                CompanyKeywordsError = "";
            }
            if (Input_CategoryComboBox.SelectedItem == null)
            {
                CompanyCategoryError = "Selecteer een categorie";
            }
            else
            {
                CompanyCategoryError = "";
            }
            if (Input_CompanyCity.Text.Length == 0)
            {
                CompanyCityError = "Stad mag niet leeg zijn";
            }
            else
            {
                CompanyCityError = "";
            }
            if (Input_CompanyPostal.Text.Length == 0)
            {
                CompanyPostalError = "Postcode mag niet leeg zijn";
            }
            else if (!int.TryParse(Input_CompanyPostal.Text, out int num))
            {
                CompanyPostalError = "Postcode moet een getal zijn";
            }
            else
            {
                CompanyPostalError = "";
            }
            if (Input_CompanyStreet.Text.Length == 0)
            {
                CompanyStreetError = "Straat mag niet leeg zijn";
            }
            else
            {
                CompanyStreetError = "";
            }
            if (Input_CompanyNumber.Text.Length == 0)
            {
                CompanyNumberError = "Nummer mag niet leeg zijn";
            }
            else if (!int.TryParse(Input_CompanyNumber.Text, out int num))
            {
                CompanyNumberError = "Nummer moet een getal zijn";
            }
            else
            {
                CompanyNumberError = "";
            }
            CompanyOpeningHoursError = "";
            if (GetOpeningHours().Count == 0) {
                CompanyOpeningHoursError = "Minstens 1 openingsdag";
            }
            else { 
                if (ou_maandag.IsChecked == null ? false : ou_maandag.IsChecked == false ? false : true && (String.IsNullOrEmpty(maandag_van.Text)|| String.IsNullOrEmpty(maandag_tot.Text)))
                {CompanyOpeningHoursError = "Elke openingsdag moet een Van en Tot uur hebben";
                }
                if (ou_dinsdag.IsChecked == null ? false : ou_dinsdag.IsChecked == false ? false : true && (String.IsNullOrEmpty(dinsdag_van.Text) || String.IsNullOrEmpty(dinsdag_tot.Text)))
                {
                    CompanyOpeningHoursError = "Elke openingsdag moet een Van en Tot uur hebben";
                }
                if (ou_woensdag.IsChecked == null ? false : ou_woensdag.IsChecked == false ? false : true && (String.IsNullOrEmpty(woensdag_van.Text) || String.IsNullOrEmpty(woensdag_tot.Text)))
                {
                    CompanyOpeningHoursError = "Elke openingsdag moet een Van en Tot uur hebben";
                }
                if (ou_donderdag.IsChecked == null ? false : ou_donderdag.IsChecked == false ? false : true && (String.IsNullOrEmpty(donderdag_van.Text) || String.IsNullOrEmpty(donderdag_tot.Text)))
                {
                    CompanyOpeningHoursError = "Elke openingsdag moet een Van en Tot uur hebben";
                }
                if (ou_vrijdag.IsChecked == null ? false : ou_vrijdag.IsChecked == false ? false : true && (String.IsNullOrEmpty(vrijdag_van.Text) || String.IsNullOrEmpty(vrijdag_tot.Text)))
                {
                    CompanyOpeningHoursError = "Elke openingsdag moet een Van en Tot uur hebben";
                }
                if (ou_zaterdag.IsChecked == null ? false : ou_zaterdag.IsChecked == false ? false : true && (String.IsNullOrEmpty(zaterdag_van.Text) || String.IsNullOrEmpty(zaterdag_tot.Text)))
                {
                    CompanyOpeningHoursError = "Elke openingsdag moet een Van en Tot uur hebben";
                }
                if (ou_zondag.IsChecked == null ? false : ou_zondag.IsChecked == false ? false : true && (String.IsNullOrEmpty(zondag_van.Text) || String.IsNullOrEmpty(zondag_tot.Text)))
                {
                    CompanyOpeningHoursError = "Elke openingsdag moet een Van en Tot uur hebben";
                }
            }
            Bindings.Update();
        }

        private List<OpeningHours> GetOpeningHours()
        {
            List<OpeningHours> oh = new List<OpeningHours>();
            if(ou_maandag.IsChecked == null ? false : ou_maandag.IsChecked == false ? false : true){
                oh.Add(new OpeningHours(Days.Maandag, maandag_van.Text, maandag_tot.Text));
            }
            if (ou_dinsdag.IsChecked == null ? false : ou_dinsdag.IsChecked == false ? false : true)
            {
                oh.Add(new OpeningHours(Days.Dinsdag, dinsdag_van.Text, dinsdag_tot.Text));
            }
            if (ou_woensdag.IsChecked == null ? false : ou_woensdag.IsChecked == false ? false : true)
            {
                oh.Add(new OpeningHours(Days.Woensdag, woensdag_van.Text, woensdag_tot.Text));
            }
            if (ou_donderdag.IsChecked == null ? false : ou_donderdag.IsChecked == false ? false : true)
            {
                oh.Add(new OpeningHours(Days.Donderdag, donderdag_van.Text, donderdag_tot.Text));
            }
            if (ou_vrijdag.IsChecked == null ? false : ou_vrijdag.IsChecked == false ? false : true)
            {
                oh.Add(new OpeningHours(Days.Vrijdag, vrijdag_van.Text, vrijdag_tot.Text));
            }
            if (ou_zaterdag.IsChecked == null ? false : ou_zaterdag.IsChecked == false ? false : true)
            {
                oh.Add(new OpeningHours(Days.Zaterdag, zaterdag_van.Text, zaterdag_tot.Text));
            }
            if (ou_zondag.IsChecked == null ? false : ou_zondag.IsChecked == false ? false : true)
            {
                oh.Add(new OpeningHours(Days.Zondag, zondag_van.Text, zondag_tot.Text));
            }
            return oh;
        }

        private void Days_Checked(object sender, RoutedEventArgs e)
        {
            Mau = ou_maandag.IsChecked == null ? false : ou_maandag.IsChecked == false ? false : true;
            Diu = ou_dinsdag.IsChecked == null ? false : ou_dinsdag.IsChecked == false ? false : true;
            Wou = ou_woensdag.IsChecked == null ? false : ou_woensdag.IsChecked == false ? false : true;
            Dou = ou_donderdag.IsChecked == null ? false : ou_donderdag.IsChecked == false ? false : true;
            Vru = ou_vrijdag.IsChecked == null ? false : ou_vrijdag.IsChecked == false ? false : true;
            Zau = ou_zaterdag.IsChecked == null ? false : ou_zaterdag.IsChecked == false ? false : true;
            Zou = ou_zondag.IsChecked == null ? false : ou_zondag.IsChecked == false ? false : true;
        }
    }
}
