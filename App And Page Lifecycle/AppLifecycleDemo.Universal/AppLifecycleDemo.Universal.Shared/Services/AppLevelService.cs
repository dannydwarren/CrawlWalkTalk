using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI.Xaml;

namespace AppLifecycleDemo.Universal.Services
{
    public class AppLevelService : INotifyPropertyChanged
    {
        #region Singleton
        private static AppLevelService _instance;
        public static AppLevelService Instance
        {
            get { return _instance ?? (_instance = new AppLevelService()); }
        }
        private AppLevelService()
        {
            _timer.Tick += TimerTickHandler;
        }

        #endregion

        private readonly DispatcherTimer _timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1),
        };
        
        private void TimerTickHandler( object sender, object e )
        {
            SecondsSinceAppLaunch++;
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
