using System;
using System.Threading.Tasks;
using Autofac;
using CityApp.Views;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI.Xaml.Controls;

namespace CityApp.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        #region === Fields === 
        private bool _isNavigating;
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
            // PageViewModels = new Dictionary<Type, NavigatedToViewModelDelegate>();
            // RegisterPageViewModel<Home, HomeViewModel>();

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
            return NavigateToPage<TPage>(parameter: null);
        }

        private async Task NavigateToPage<TPage>(object parameter)
        {
            //// Early out if already in the middle of a Navigation
            //if (_isNavigating)
            //{
            //    return;
            //}

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

        public Task NavigateToAccountAsync()
        {
            return NavigateToPage<Account>();
        }
        #endregion
    }
}
