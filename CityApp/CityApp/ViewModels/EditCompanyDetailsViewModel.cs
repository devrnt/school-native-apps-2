using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;
using CityApp.Services.Rest;
using Windows.UI.Xaml.Navigation;

namespace CityApp.ViewModels
{
    public class EditCompanyDetailsViewModel : INavigableTo, INotifyPropertyChanged
    {
        public Company Company { get; private set; }
        public ObservableCollection<Promotion> Promotions { get; set; }
        public ObservableCollection<Discount> Discounts { get; set; }
        public ObservableCollection<Event> Events { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RelayCommand DeletePromotionCommand { get; set; }
        public RelayCommand DeleteDiscountCommand { get; set; }

        private INavigationService _navigationService;
        private UserService _userService;
        private CompanyService _companyService;


        public EditCompanyDetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _userService = UserService.us;
            _companyService = new CompanyService();
            DeletePromotionCommand = new RelayCommand((p) => DeletePromotions());
            DeleteDiscountCommand = new RelayCommand((p) => DeleteDiscounts());
        }
        public async void AddEventAsync(string t, string d, DateTime date, string i)
        {
            var @event = new Event(t, d, date, i);

            var eventResult = await _companyService.AddEvent(Company.Id, @event);
            Events.Add(eventResult);
            AlertService.Toast("Event toegevoegd", $"Het event {eventResult.Title} toegevoegd");
            //Company.Events.Add(@event);
        }
        public async void AddPromotionAsync(String s, Object d)
        {
            var promotion = new Promotion(s, (Discount)d);

            var promotionResult = await _companyService.AddPromotion(Company.Id, promotion);
            Promotions.Add(promotionResult);
            AlertService.Toast("Promotie toegevoegd", $"De promotie {promotionResult.Description} toegevoegd");
        }
        public void AddDiscount(string c, string pdf)
        {
            Discount d = new Discount(c, pdf);
            Discounts.Add(d);
            Company.Discounts.Add(d);
        }

        private void DeletePromotions()
        {
            Promotions.Clear();
            Company.Promotions.Clear();
        }
        private void DeleteDiscounts()
        {
            Discounts.Clear();
            Company.Discounts.Clear();
        }
        Task INavigableTo.NavigatedTo(NavigationMode navigationMode, object parameter)
        {
            if (navigationMode != NavigationMode.Back && parameter is Company company)
            {
                Company = company;
                Events = new ObservableCollection<Event>();
                foreach (Event e in company.Events)
                {
                    Events.Add(e);
                }
                Promotions = new ObservableCollection<Promotion>();
                foreach (Promotion p in company.Promotions)
                {
                    Promotions.Add(p);
                }
                Discounts = new ObservableCollection<Discount>();
                foreach (Discount d in company.Discounts)
                {
                    Discounts.Add(d);
                }
            }
            return Task.FromResult<object>(null);
        }

        public async void DeleteCompanyAsync()
        {
            var company = await _companyService.RemoveCompany(Company.Id);

            await _navigationService.NavigateToAccountAsync();
            AlertService.Toast("Bedrijf verwijderd", $"Het bedrijf {company.Description} succesvol verwijderd");
        }
    }
}
