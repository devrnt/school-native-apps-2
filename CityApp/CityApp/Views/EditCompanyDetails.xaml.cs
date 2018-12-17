using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CityApp.Services.Navigation;
using CityApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class EditCompanyDetails : Page, IPageWithViewModel<EditCompanyDetailsViewModel>
    {
        public EditCompanyDetailsViewModel ViewModel { get; set; }
        private bool _isNewEventVisivle;
        public bool IsNewEventVisible
        {
            get { return _isNewEventVisivle; }
            set
            {
                _isNewPromoVisivle = value;
                Bindings.Update();
            }
        }
        private bool _isNewPromoVisivle;
        public bool IsNewPromoVisible
        {
            get { return _isNewPromoVisivle; }
            set
            {
                _isNewPromoVisivle = value;
                Bindings.Update();
            }
        }
        private bool _isNewDiscountVisible;
        public bool IsNewDiscountVisible
        {
            get { return _isNewDiscountVisible; }
            set
            {
                _isNewDiscountVisible = value;
                Bindings.Update();
            }
        }

        private bool _isAddEventVisible;
        public bool IsAddEventVisible
        {
            get { return _isAddEventVisible; }
            set
            {
                _isAddEventVisible = value;
                Bindings.Update();
            }
        }
        private bool _isAddPromotionVisible;
        public bool IsAddPromotionVisible
        {
            get { return _isAddPromotionVisible; }
            set
            {
                _isAddPromotionVisible = value;
                Bindings.Update();
            }
        }
        private bool _isAddDiscountVisible;
        public bool IsAddDiscountVisible
        {
            get { return _isAddDiscountVisible; }
            set
            {
                _isAddDiscountVisible = value;
                Bindings.Update();
            }
        }

        private bool _showhideEvents;
        public bool ShowhideEvents
        {
            get { return _showhideEvents; }
            set
            {
                _showhideEvents = value;
                Bindings.Update();
            }
        }
        private bool _showhidePromotions;
        public bool ShowhidePromotions
        {
            get { return _showhidePromotions; }
            set
            {
                _showhidePromotions = value;
                Bindings.Update();
            }
        }
        private bool _showhideDiscounts;
        public bool ShowhideDiscounts
        {
            get { return _showhideDiscounts; }
            set
            {
                _showhideDiscounts = value;
                Bindings.Update();
            }
        }

        public EditCompanyDetails()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
            IsAddDiscountVisible = false;
            IsAddPromotionVisible = false;
            IsNewPromoVisible = true;
            IsNewDiscountVisible = true;
            ShowhidePromotions = false;
        }

        private void ShowHideEvents(object sender, RoutedEventArgs e)
        {
            ShowhideEvents = !ShowhideEvents;
        }
        private void ShowHidePromoties(object sender, RoutedEventArgs e)
        {
            ShowhidePromotions = !ShowhidePromotions;
        }
        private void ShowHideDiscounts(object sender, RoutedEventArgs e)
        {
            ShowhideDiscounts = !ShowhideDiscounts;
        }

        private void NewEvents(object sender, RoutedEventArgs e)
        {
            IsAddEventVisible = !IsAddEventVisible;
            IsNewPromoVisible = !IsNewPromoVisible;
        }
        private void NewPromotion(object sender, RoutedEventArgs e)
        {
            IsAddPromotionVisible = !IsAddPromotionVisible;
            IsNewPromoVisible = !IsNewPromoVisible;
        }
        private void NewDiscount(object sender, RoutedEventArgs e)
        {
            IsAddDiscountVisible = !IsAddDiscountVisible;
            IsNewDiscountVisible = !IsNewDiscountVisible;
        }
        private void AddEvent(object sender, RoutedEventArgs e)
        {
            this.ViewModel.AddEvent(event_Titel.Text, event_Omschrijving.Text, event_Date.Date.DateTime, event_Chosenpdf.Text);
            promo_Omschrijving.Text = "";
            promo_Discounts.SelectedIndex = -1;
            IsAddPromotionVisible = false;
            IsNewPromoVisible = true;
        }
        private void AddPromotie(object sender, RoutedEventArgs e)
        {
            this.ViewModel.AddPromotion(promo_Omschrijving.Text, promo_Discounts.SelectedItem);
            promo_Omschrijving.Text = "";
            promo_Discounts.SelectedIndex = -1;
            IsAddPromotionVisible = false;
            IsNewPromoVisible = true;
        }
        private void AddDiscount(object sender, RoutedEventArgs e)
        {
            this.ViewModel.AddDiscount(disc_Kortingscode.Text, disc_Chosenpdf.Text);
            disc_Kortingscode.Text = "";
            disc_Chosenpdf.Text = "";
            IsAddDiscountVisible = false;
            IsNewDiscountVisible = true;
        }
        private void CancelAddEvent(object sender, RoutedEventArgs e)
        {
            event_Titel.Text = "";
            event_Omschrijving.Text = "";
            event_Chosenpdf.Text = "Geen bestand gekozen";
            IsAddEventVisible = false;
            IsNewEventVisible = true;
        }
        private void CancelAddPromotie(object sender, RoutedEventArgs e)
        {
            promo_Omschrijving.Text = "";
            promo_Discounts.SelectedIndex = -1;
            IsAddPromotionVisible = false;
            IsNewPromoVisible = true;
        }
        private void CancelAddDiscount(object sender, RoutedEventArgs e)
        {
            disc_Kortingscode.Text = "";
            disc_Chosenpdf.Text = "";
            IsAddDiscountVisible = false;
            IsNewDiscountVisible = true;
        }

        private async void disc_Browsepdf(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".pdf");

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                disc_Chosenpdf.Text = "Gekozen pdf" + file.Name;
            }
            else
            {
                disc_Chosenpdf.Text = "Geen bestand gekozen";
            }
        }
        private async void event_Browsepdf(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".pdf");

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                event_Chosenpdf.Text = "Gekozen img" + file.Name;
            }
            else
            {
                event_Chosenpdf.Text = "Geen bestand gekozen";
            }
        }

        private void event_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            if (event_Date.Date > DateTimeOffset.Now)
            {
                event_Date.Date = DateTime.Now;
            }
        }

        public void UpdateBindings()
        {
            if (Bindings != null)
            {
                Bindings.Update();
            }
        }
    }
}
