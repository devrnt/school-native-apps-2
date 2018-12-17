using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CityApp.Views.Company
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDiscount : Page
    {
        public AddDiscount()
        {
            this.InitializeComponent();
        }

            private async void BrowsePdf_Click(object sender, RoutedEventArgs e)
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
                    ChosenPdf_Text.Text = "Gekozen pdf" + file.Name;
                }
                else
                {
                    ChosenPdf_Text.Text = "Geen bestand gekozen";
                }
            }
        private void CreateDiscount_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Creating discount...");
        }
    }
}
