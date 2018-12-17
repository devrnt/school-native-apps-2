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


namespace CityApp.Views
{
    public sealed partial class OwnerAccount : Page, IPageWithViewModel<OwnerAccountViewModel>
    {
        public OwnerAccountViewModel ViewModel { get; set; }

        public OwnerAccount()
        {
            this.InitializeComponent();
        }

        public void UpdateBindings()
        {
            this.DataContext = ViewModel;
        }
    }
}
