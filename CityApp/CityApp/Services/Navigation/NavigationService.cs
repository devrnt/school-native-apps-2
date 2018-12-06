using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using CityApp.DataModel;
using CityApp.ViewModels;
using CityApp.Views;
using CityApp.Views.Company;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CityApp.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        #region === Fields === 
        private bool _isNavigating;
        public static NavigationService ns;
        private delegate Task NavigatedToViewModelDelegate(object page, object parameter, NavigationEventArgs navigationArgs);

        private Dictionary<Type, NavigatedToViewModelDelegate> PageViewModels { get; }
        #endregion

        #region === Properties === 
        public IFrameAdapter Frame { get; set; }
        private IComponentContext AutofacDepedencyResolver { get; }
        public bool CanGoBack => Frame.CanGoBack;

        public bool IsNavigating
        {
            get => _isNavigating;

            set
            {
                if (value != _isNavigating)
                {
                    _isNavigating = value;
                    IsNavigatingChanged?.Invoke(this, _isNavigating);

                    if (!_isNavigating)
                    {
                        Navigated?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public event EventHandler<bool> IsNavigatingChanged;
        public event EventHandler Navigated;
        #endregion

        #region === Constructor ===
        public NavigationService(IFrameAdapter frameAdapter, IComponentContext iocResolver)
        {
            Frame = frameAdapter;
            // Autofac is an IoC container for Microsoft .NET. 
            // It manages the dependencies between classes 
            // so that applications stay easy to change as they grow in size and complexity.
            AutofacDepedencyResolver = iocResolver;

            // register viewmodels here
            PageViewModels = new Dictionary<Type, NavigatedToViewModelDelegate>();
            RegisterPageViewModel<Companies, CompaniesViewModel>();
            RegisterPageViewModel<SettingsPage, SettingsViewModel>();
            RegisterPageViewModel<CompanyDetails, CompanyDetailsViewModel>();

            Frame.Navigated += Frame_Navigated;
            ns = this;
        }
        #endregion

        #region === Methods ===
        public async Task GoBackAsync()
        {
            if (Frame.CanGoBack)
            {
                IsNavigating = true;

                Page navigatedPage = await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    Frame.GoBack();
                    return Frame.Content as Page;
                });
            }
        }

        private Task NavigateToPage<TPage>()
        {
            return NavigateToPage<TPage>(null);
        }

        private async Task NavigateToPage<TPage>(object parameter)
        {
            // Early out if already in the middle of a Navigation
            if (_isNavigating)
            {
                return;
            }

            _isNavigating = true;

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Frame.Navigate(typeof(TPage), parameter: parameter);
            });
        }

        public Task NavigateToCompaniesAsync()
        {
            return NavigateToPage<Companies>();
        }

        public Task NavigateToCompanyDetailsAsync(Company c)
        {
            return NavigateToPage<CompanyDetails>(c);
        }

        public Task NavigateToAccountAsync()
        {
            return NavigateToPage<Account>();
        }

        public Task NavigateToAddCompanyAsync()
        {
            return NavigateToPage<AddCompany>();
        }

        public Task NavigateToAddPromotionAsync()
        {
            return NavigateToPage<AddPromotion>();
        }

        public Task NavigateToAddDiscountAsync()
        {
            return NavigateToPage<AddDiscount>();
        }

        public Task NavigateToSettingsAsync()
        {
            return NavigateToPage<SettingsPage>();
        }
        public Task NavigateToRegisterAsync()
        {
            return NavigateToPage<Register>();
        }

        private void RegisterPageViewModel<TPage, TViewModel>()
           where TViewModel : class
        {
            NavigatedToViewModelDelegate navigatedTo = async (page, parameter, navArgs) =>
            {
                if (page is IPageWithViewModel<TViewModel> pageWithVM)
                {
                    pageWithVM.ViewModel = AutofacDepedencyResolver.Resolve<TViewModel>();

                    if (pageWithVM.ViewModel is INavigableTo navVM)
                    {
                        await navVM.NavigatedTo(navArgs.NavigationMode, parameter);
                    }

                    // Async loading
                    //pageWithVM.UpdateBindings();
                }
            };

            PageViewModels[typeof(TPage)] = navigatedTo;
        }

        /// <summary>
        /// The Navigated event. This event is raised BEFORE <see cref="Windows.UI.Xaml.Controls.Page.OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs)"/>
        /// </summary>
        /// <param name="sender">The frame</param>
        /// <param name="e">The args coming from the frame</param>
        private void Frame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            IsNavigating = false;
            if (PageViewModels.ContainsKey(e.SourcePageType))
            {
                var loadViewModelDelegate = PageViewModels[e.SourcePageType];
                var ignoredTask = loadViewModelDelegate(e.Content, e.Parameter, e);
            }
        }

        #endregion
    }
}
