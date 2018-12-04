﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CityApp.DataModel;
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

namespace CityApp.Views.Company
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPromotion : Page
    {
        // company passed by the previous page to add the promotion
        public CityApp.DataModel.Company Company { get; set; }

        public AddPromotion()
        {
            // Tijdelijk, zal altijd een valid company zijn
            Company = DummyDataSource.Companies[0];
            this.InitializeComponent();
        }

        private void CreatePromotion_Click(object sender, RoutedEventArgs e)
        {
            var promotion = new Promotion(Input_Omschrijving.Text);

            Company.Promotions.Add(promotion);
            // post the new company to the REST api
            
        }
    }
}