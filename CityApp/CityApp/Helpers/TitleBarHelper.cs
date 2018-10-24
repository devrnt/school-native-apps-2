using System;
using System.ComponentModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace CityApp.Helpers
{
    public class TitleBarHelper : INotifyPropertyChanged
    {
        private static readonly TitleBarHelper _instance = new TitleBarHelper();
        // custom title bar
        private static CoreApplicationViewTitleBar _coreTitleBar;
        private Thickness _titlePosition;
        private Visibility _titleVisibility;
        // INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        public static TitleBarHelper Instance
        {
            get
            {
                return _instance;
            }
        }

        public CoreApplicationViewTitleBar TitleBar
        {
            get
            {
                return _coreTitleBar;
            }
        }

        public Thickness TitlePosition
        {
            get
            {
                return _titlePosition;
            }
            set
            {
                if (value.Left != _titlePosition.Left || value.Top != _titlePosition.Top)
                {
                    _titlePosition = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TitlePosition)));
                }
            }
        }

        public Visibility TitleVisibility
        {
            get
            {
                return _titleVisibility;
            }

            set
            {
                _titleVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TitleVisibility)));
            }
        }

        public TitleBarHelper()
        {
            _coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            _coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            _titlePosition = CalculateTilebarOffset(_coreTitleBar.SystemOverlayLeftInset, _coreTitleBar.Height);
            _titleVisibility = Visibility.Visible;
        }

        public void ExitFullscreen()
        {
            TitleVisibility = Visibility.Visible;
        }

        public void GoFullscreen()
        {
            TitleVisibility = Visibility.Collapsed;
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            TitlePosition = CalculateTilebarOffset(_coreTitleBar.SystemOverlayLeftInset, _coreTitleBar.Height);
        }

        private Thickness CalculateTilebarOffset(double leftPosition, double height)
        {
            // top position should be 6 pixels for a 32 pixel high titlebar hence scale by actual height
            double correctHeight = height / 32 * 6;
            return new Thickness(leftPosition + 12, correctHeight, 0, 0);
        }
    }
}
