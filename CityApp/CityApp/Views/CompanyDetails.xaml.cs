﻿using CityApp.Services.Navigation;
using CityApp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CityApp.Views
{
    public sealed partial class CompanyDetails : Page, IPageWithViewModel<CompanyDetailsViewModel>
    {
        public CompanyDetailsViewModel ViewModel { get; set; }

        public CompanyDetails()
        {
            this.InitializeComponent();
        }

    }
}
