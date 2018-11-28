namespace CityApp.Views
{
    using System;
    using CityApp.Helpers;
    using CityApp.Services;
    using Microsoft.Toolkit.Uwp.Helpers;
    using Windows.System.Profile;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class NavigationRoot : Page
    {
        private static NavigationRoot _instance;
        private INavigationService _navigationService;
        private bool hasLoadedPreviously;

        public NavigationRoot()
        {
            _instance = this;
            InitializeComponent();

            var nav = SystemNavigationManager.GetForCurrentView();

            nav.BackRequested += Nav_BackRequested;
        }

        public static NavigationRoot Instance
        {
            get
            {
                return _instance;
            }
        }

        public Frame AppFrame
        {
            get
            {
                return appNavFrame;
            }
        }

        public TitleBarHelper TitleHelper
        {
            get
            {
                return TitleBarHelper.Instance;
            }
        }

        public void InitializeNavigationService(INavigationService navigationService)
        {
            _navigationService = navigationService;
            // TODO: Hook into Navigation Events for loading screen
            _navigationService.Navigated += NavigationService_Navigated;
        }

        public void ToggleFullscreen()
        {
            ViewModeService.Instance.ToggleFullscreen();
        }

        public void ExitFullScreen()
        {
            ViewModeService.Instance.DoExitFullscreen();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //if (e.Parameter is Episode)
            //{
            //    AppFrame.Navigate(typeof(Player), e.Parameter);
            //}
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModeService.Instance.UnRegister();
        }

        private void Nav_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var ignored = _navigationService.GoBackAsync();
            e.Handled = true;
        }

        private void NavigationService_Navigated(object sender, EventArgs e)
        {
            var ignored = DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                var nav = SystemNavigationManager.GetForCurrentView();
                nav.AppViewBackButtonVisibility = _navigationService.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            });
        }

        private void AppNavFrame_Navigated(object sender, NavigationEventArgs e)
        {
            switch (e.SourcePageType)
            {
                case Type c when e.SourcePageType == typeof(Companies):
                    ((NavigationViewItem)navview.MenuItems[0]).IsSelected = true;
                    break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Only do an inital navigate the first time the page loads
            // when we switch out of compactoverloadmode this will fire but we don't want to navigate because
            // there is already a page loaded
            if (!hasLoadedPreviously)
            {
                // _navigationService.NavigateToPodcastsAsync();
                hasLoadedPreviously = true;
            }

            ViewModeService.Instance.Register(navview, appNavFrame);

            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Xbox")
            {
                ViewModeService.Instance.CollapseNavigationViewToBurger();
                TitleBarHelper.Instance.TitleVisibility = Visibility.Collapsed;
            }
        }

        private void MenuItem_Invoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            // If settings is clicked in navigation view
            if (args.IsSettingsInvoked)
            {
                _navigationService.NavigateToSettingsAsync();
                return;
            }

            // This is not safe
            // Looking for an alternitive to get a String in the resources
            switch (args.InvokedItem as string)
            {
                case "Lokale handelaars":
                    _navigationService.NavigateToCompaniesAsync();
                    break;
                case "Account":
                    _navigationService.NavigateToAccountAsync();
                    break;
                case "[Tijdelijk] bedrijf toevoegen":
                    _navigationService.NavigateToAddCompanyAsync();
                    break;


            }
        }

        #region === Binding Helpers ===
        public static string GetIcon(string kind)
        {
            switch (kind)
            {
                case "Phone":
                    return "\uE8EA";
                case "Xbox":
                    return "\uE7FC";
                default:
                    return "\uE770";
            }
        }

        public static string ShortDate(DateTime d)
        {
            return d.ToString("d");
        }
        #endregion
    }
}
