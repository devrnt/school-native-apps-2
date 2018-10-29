using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.ApplicationModel;

namespace CityApp.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged

    {
        private string _versionDescription;

        public SettingsViewModel()
        {
            // _versionDescription = GetVersionDescription();
            _versionDescription = "pre-v0.0.0";
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

            return $"{package.DisplayName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
