using System.Collections.ObjectModel;
using System.ComponentModel;
using CityApp.DataModel;
using CityApp.Services;

namespace CityApp.ViewModels
{
    public class CompaniesViewModel : INotifyPropertyChanged
    {
        #region === Fields ===
        private INavigationService _navigationService;
        private ObservableCollection<Company> _companies;
        public ObservableCollection<Company> Companies { get => _companies; set => _companies = value; }
        // add the list of companies here in future
        // IQueryable<Company> _companies;
        #endregion

        #region === Properties ===
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region === Constructor ===
        public CompaniesViewModel() {
            Companies = new ObservableCollection<Company>();
            Companies.Add(new Company(5, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp2", "Beschrijving comp2", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp2", "Beschrijving comp2", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp2", "Beschrijving comp2", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp2", "Beschrijving comp2", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp2", "Beschrijving comp2", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null));
            Companies.Add(new Company(5, "comp2", "Beschrijving comp2", null, Categories.Bank, null, null, null, null, null, null));

        }
        public CompaniesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        #endregion

        #region === Methods ===
        // Imagine we have a button on the Companies.xaml with a redirection to another page
        // Then:
        // Task NavigateToWhateverPageAsync() {_navigatinService.NavigateToWhateverPage()}
        // Awesome isn't it?
        #endregion
    }
}
