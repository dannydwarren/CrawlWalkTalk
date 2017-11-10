using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AppLifecycleDemo.Uwp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace AppLifecycleDemo.Uwp.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            DebugWrite.MethodName(nameof(MainPage));
            this.InitializeComponent();
            DataContext = AppLevelService.Instance;
            _timer.Tick += TimerTickHandler;
            _timer.Start();
        }

        private readonly DispatcherTimer _timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1),
        };

        private void TimerTickHandler(object sender, object e)
        {
            SecondsOnPage++;
        }


        private int _secondsOnPage;
        public int SecondsOnPage
        {
            get { return _secondsOnPage; }
            private set
            {
                if (_secondsOnPage.Equals(value))
                {
                    return;
                }
                _secondsOnPage = value;

                NotifyPropertyChanged();
            }
        }

        private void PartA_Click( object sender, RoutedEventArgs routedEventArgs )
        {
            Navigate("A");
        }

        private void PartB_Click( object sender, RoutedEventArgs routedEventArgs )
        {
            Navigate("B");
        }

        private void PartC_Click( object sender, RoutedEventArgs routedEventArgs )
        {
            Navigate("C");
        }

        private void PartD_Click( object sender, RoutedEventArgs routedEventArgs )
        {
            Navigate("D");
        }

        private void PartE_Click( object sender, RoutedEventArgs routedEventArgs )
        {
            Navigate("E");
        }

        private void Navigate( string stateParameter )
        {
            //TODO: AppLifecycleDemo 1.0 - Navigate

            DebugWrite.MethodName(nameof(MainPage));
            Frame.Navigate(typeof(ContentPage), stateParameter);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
