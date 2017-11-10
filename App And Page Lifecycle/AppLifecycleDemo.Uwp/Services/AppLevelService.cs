using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

namespace AppLifecycleDemo.Uwp.Services
{
    public class AppLevelService : INotifyPropertyChanged
    {
        private static AppLevelService _instance;
        public static AppLevelService Instance
        {
            get { return _instance ?? (_instance = new AppLevelService()); }
        }
        private AppLevelService()
        {
            _timer.Tick += TimerTickHandler;
        }

        private readonly DispatcherTimer _timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1),
        };

        private void TimerTickHandler( object sender, object e )
        {
            SecondsSinceAppLaunch++;
            SecondsAppInUse++;
        }


        private int _secondsSinceAppLaunch;
        public int SecondsSinceAppLaunch
        {
            get { return _secondsSinceAppLaunch; }
            private set
            {
                if (_secondsSinceAppLaunch.Equals(value))
                {
                    return;
                }
                _secondsSinceAppLaunch = value;

                NotifyPropertyChanged();
            }
        }

        private int _secondsAppInUse;
        public int SecondsAppInUse
        {
            get { return _secondsAppInUse; }
            set
            {
                if (_secondsAppInUse.Equals(value))
                {
                    return;
                }
                _secondsAppInUse = value;

                NotifyPropertyChanged();
            }
        }


        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged( [CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
