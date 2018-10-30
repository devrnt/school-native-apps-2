using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using CityApp.DataModel;
using CityApp.Helpers;
using CityApp.Services;
using Windows.ApplicationModel;

namespace CityApp.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged

    {
        private string _versionDescription;
        private Themes _elementTheme = ThemeSelectorService.Theme;
        private ICommand _switchThemeCommand;

        public Themes Theme
        {
            get
            {
                return _elementTheme;
            }

            set
            {
                if (_elementTheme != value)
                {
                    _elementTheme = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VersionDescription)));
                }
            }
        }

        public SettingsViewModel()
        {
            _versionDescription = GetVersionDescription();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string VersionDescription
        {
            get
            {
                return _versionDescription;
            }

            set
            {
                if (_versionDescription != value)
                {
                    _versionDescription = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VersionDescription)));
                }
            }
        }

        public void Initialize()
        {
            VersionDescription = GetVersionDescription();
        }

        private string GetVersionDescription()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"prebuild-{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }



        public ICommand SwitchThemeCommand
        {
            get
            {
                return _switchThemeCommand ?? (_switchThemeCommand = new RelayCommand<Themes>(
                        async (param) =>
                        {
                            await ThemeSelectorService.SetThemeAsync(param);
                        }));
            }
        }
    }
}
