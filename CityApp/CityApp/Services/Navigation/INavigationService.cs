using System;
using System.Threading.Tasks;
using CityApp.DataModel;

namespace CityApp.Services
{
    public interface INavigationService
    {
        event EventHandler<bool> IsNavigatingChanged;

        event EventHandler Navigated;

        bool CanGoBack { get; }

        bool IsNavigating { get; }

        // Add Tasks to handle navigation menu
        Task NavigateToCompaniesAsync();
        Task NavigateToCompanyDetailsAsync(Company c);
        Task NavigateToAccountAsync();
        Task NavigateToSettingsAsync();
        Task NavigateToAddCompanyAsync();
        Task NavigateToAddPromotionAsync();
        Task NavigateToAddDiscountAsync();
        Task NavigateToRegisterAsync();
        Task GoBackAsync();
    }
}
