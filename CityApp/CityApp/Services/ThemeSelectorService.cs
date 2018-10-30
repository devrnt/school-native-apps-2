using System;
using System.Threading.Tasks;
using CityApp.DataModel;
using CityApp.Helpers;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace CityApp.Services
{
    public static class ThemeSelectorService
    {
        private const string SettingsKey = "RequestedTheme";

        private static ResourceDictionary _customTheme = new ResourceDictionary { Source = new Uri("ms-appx:///Themes/Branded.xaml", UriKind.Absolute) };
        private static ResourceDictionary _stockTheme = new ResourceDictionary { Source = new Uri("ms-appx:///Themes/Stock.xaml", UriKind.Absolute) };

        public static event EventHandler<Themes> OnThemeChanged = (sender, args) => { };

        public static Themes Theme { get; set; } = Themes.Default;

        public static string GetSystemControlForegroundColorForThemeHex()
        {
            if (TrueTheme() == ElementTheme.Dark)
            {
                return "#FFFFFF";
            }
            else
            {
                return "#000000";
            }
        }
        public static async Task InitializeAsync()
        {
            Theme = await LoadThemeFromSettingsAsync();
        }
        public static async Task SetThemeAsync(Themes theme)
        {
            Theme = theme;

            SetRequestedTheme();
            await SaveThemeInSettingsAsync(Theme);

            OnThemeChanged(null, Theme);
        }

        public static void SetRequestedTheme()
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                ElementTheme trueTheme;

                if (Theme == Themes.Custom)
                {
                    if (Application.Current.Resources.MergedDictionaries.Contains(_stockTheme))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(_stockTheme);
                    }

                    Application.Current.Resources.MergedDictionaries.Add(_customTheme);

                    trueTheme = ElementTheme.Dark;

                    if (frameworkElement.RequestedTheme == ElementTheme.Dark)
                    {
                        frameworkElement.RequestedTheme = ElementTheme.Light;
                    }
                }
                else
                {
                    if (Application.Current.Resources.MergedDictionaries.Contains(_customTheme))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(_customTheme);
                    }

                    // for the case we switch between light and dark which share stock
                    if (!Application.Current.Resources.MergedDictionaries.Contains(_stockTheme))
                    {
                        Application.Current.Resources.MergedDictionaries.Add(_stockTheme);
                    }

                    trueTheme = (ElementTheme)Theme;

                    if (frameworkElement.RequestedTheme == ElementTheme.Dark)
                    {
                        frameworkElement.RequestedTheme = ElementTheme.Light;
                    }
                }

                frameworkElement.RequestedTheme = trueTheme;
            }

            SetupTitlebar();
        }

        public static string GetLogoSource()
        {
            if (Theme == Themes.Dark)
            {
                // return other logo in black theme
            }

            // return logo
            return "";
        }

        public static string GetHomeImageSource()
        {
            if (Theme == Themes.Dark)
            {
                // return other logo in black theme
            }

            // return logo
            return "";
        }

        public static ElementTheme GetHomeTheme()
        {
            if (Theme == Themes.Custom)
            {
                return ElementTheme.Light;
            }

            return TrueTheme();
        }

        public static Style GetHomeBackground()
        {
            if (Theme == Themes.Custom)
            {
                return Application.Current.Resources["HomePageBackground"] as Style;
            }

            return Application.Current.Resources["PageBackground"] as Style;
        }

        public static ElementTheme TrueTheme()
        {
            var frameworkElement = Window.Current.Content as FrameworkElement;
            return frameworkElement.ActualTheme;
        }

        private static void SetupTitlebar()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = Colors.Transparent;
                    if (TrueTheme() == ElementTheme.Dark)
                    {
                        titleBar.ButtonForegroundColor = Colors.White;
                        titleBar.ForegroundColor = Colors.White;
                    }
                    else
                    {
                        titleBar.ButtonForegroundColor = Colors.Black;
                        titleBar.ForegroundColor = Colors.Black;
                    }

                    titleBar.BackgroundColor = Colors.Black;

                    titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                    titleBar.ButtonInactiveForegroundColor = Colors.LightGray;

                    CoreApplicationViewTitleBar coreTitleBar = TitleBarHelper.Instance.TitleBar;

                    coreTitleBar.ExtendViewIntoTitleBar = true;
                }
            }
        }

        private static async Task<Themes> LoadThemeFromSettingsAsync()
        {
            Themes cacheTheme = Themes.Default;
            string themeName = await ApplicationData
                .Current
                .LocalSettings
                .ReadAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(themeName))
            {
                Enum.TryParse(themeName, out cacheTheme);
            }

            return cacheTheme;
        }

        private static async Task SaveThemeInSettingsAsync(Themes theme)
        {
            await ApplicationData
                .Current
                .LocalSettings
                .SaveAsync(SettingsKey, theme.ToString());
        }

    }
}
