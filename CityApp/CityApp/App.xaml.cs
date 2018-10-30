using System;
using System.Threading.Tasks;
using Autofac;
using CityApp.Services;
using CityApp.Services.Navigation;
using CityApp.ViewModels;
using CityApp.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CityApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        #region === Fields ===
        private IContainer _container;
        private BackgroundTaskDeferral _appServiceDeferral;
        private NavigationRoot _rootPage;
        #endregion

        #region === Properties ===
        public static AppServiceConnection Connection { get; set; }
        #endregion

        #region === Constructor ===
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            // Sets supported mouse mode
            this.RequiresPointerMode = ApplicationRequiresPointerMode.WhenRequested;
        }
        #endregion

        #region === Methods ===
        /// <summary>
        /// Gets the visual root of the application windows.
        /// depends on the <see cref="Window.Current.Content"/>.
        /// </summary>
        /// <exception cref="Exception">
        /// Thrown when windows content is of unknown type
        /// </exception>
        public NavigationRoot GetNavigationRoot()
        {
            if (Window.Current.Content is NavigationRoot)
            {
                return Window.Current.Content as NavigationRoot;
            }
            else if (Window.Current.Content is Frame)
            {
                return ((Frame)Window.Current.Content).Content as NavigationRoot;
            }

            throw new Exception("Window content is unknown type");
        }

        public Frame GetFrame()
        {
            var root = GetNavigationRoot();
            return root.AppFrame;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            // XBOX support
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Xbox")
            {
                ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
                bool result = ApplicationViewScaling.TrySetDisableLayoutScaling(true);
            }

            await InitializeAsync();
            InitWindow(skipWindowCreation: args.PrelaunchActivated);

            // Tasks after activation
            await StartupAsync();
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await InitializeAsync();
            InitWindow(skipWindowCreation: false);

            if (args.Kind == ActivationKind.Protocol)
            {
                Window.Current.Activate();

                // Tasks after activation
                await StartupAsync();
            }
        }

        private void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            var deferral = args.GetDeferral();
            Console.WriteLine(args.Request.Message);
            deferral.Complete();
            _appServiceDeferral.Complete();
        }

        private void OnAppServicesCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            _appServiceDeferral.Complete();
        }

        private void AppServiceConnection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            _appServiceDeferral.Complete();
        }

        private void InitWindow(bool skipWindowCreation)
        {
            var builder = new ContainerBuilder();

            _rootPage = Window.Current.Content as NavigationRoot;
            bool initApp = _rootPage == null && !skipWindowCreation;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (initApp)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                _rootPage = new NavigationRoot();

                FrameAdapter adapter = new FrameAdapter(_rootPage.AppFrame);

                builder.RegisterInstance(adapter)
                       .AsImplementedInterfaces();

                // Register the view models to the builder 
                builder.RegisterType<CompaniesViewModel>();
                builder.RegisterType<AccountViewModel>();
                builder.RegisterType<SettingsViewModel>();

                builder.RegisterType<NavigationService>()
                        .AsImplementedInterfaces()
                        .SingleInstance();

                _container = builder.Build();
                _rootPage.InitializeNavigationService(_container.Resolve<INavigationService>());

                adapter.NavigationFailed += OnNavigationFailed;

                // Place the frame in the current Window
                Window.Current.Content = _rootPage;

                Window.Current.Activate();
            }
        }

        private async Task InitializeAsync()
        {
            // checks what theme was selected previous time
            await ThemeSelectorService.InitializeAsync();
            await Task.CompletedTask;
        }

        private async Task StartupAsync()
        {
            // sets the theme that was fetched in method above
            ThemeSelectorService.SetRequestedTheme();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            deferral.Complete();
        }
        #endregion
    }
}
