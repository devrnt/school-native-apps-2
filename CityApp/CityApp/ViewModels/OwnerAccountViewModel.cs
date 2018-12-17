using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.DataModel.Responses;
using CityApp.Helpers;
using CityApp.Services;
using CityApp.Services.Navigation;
using CityApp.Services.Rest;

namespace CityApp.ViewModels
{
    public class OwnerAccountViewModel
    {
        public UserResponse User { get; private set; }
        public string NameText;
        public string FirstName;

        public RelayCommand LogOutCommand { get; set; }
        public RelayCommand AddCompanyCommand { get; set; }
        public RelayCommand EditCompanyDetailsCommand { get; set; }
        public ObservableCollection<Company> Companies { get; set; }

        public OwnerAccountViewModel()
        {
            LogOutCommand = new RelayCommand((p) => ClearStoredUser());
            AddCompanyCommand = new RelayCommand((p) => AddCompany());
            EditCompanyDetailsCommand = new RelayCommand((p) => EditCompanyDetails((Company)p));

            Companies = new ObservableCollection<Company>();
            LoadUser();
        }
        private async void ClearStoredUser()
        {
            await UserService.LogOutUserAsync();
            await NavigationService.ns.NavigateToAccountAsync();
        }
        private async void LoadUser()
        {
            var user = await UserService.us.GetUser();
            User = user;
            foreach (Company c in User.Companies)
            {
                Companies.Add(c);
            }
            NameText = "Gebruiker:" + user.Name + user.FirstName;
        }
        public void AddCompany() {
            NavigationService.ns.NavigateToAddCompanyAsync();
        }
        public async void EditCompanyDetails(Company p)
        {
            var x = new CompanyService();
            var company = await x.GetCompany(p.Id);
            var c = (Company)company;
            await NavigationService.ns.NavigateToEditCompanyDetailsAsync(c);
        }
    }
}
