using AppLifecycleDemo.Uwp.Services;
using AppLifecycleDemo.Uwp.Views;
using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AppLifecycleDemo.Uwp
{
    sealed partial class App : Application
    {
        private const string NAVIGATION_STATE = "NAVIGATION_STATE";
        private const string APP_LEVEL_SERVICE_STATE = "APP_LEVEL_SERVICE_STATE";

        public App()
        {
            DebugWrite.MethodName(nameof(App));
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Resuming += App_Resuming;
        }

        void App_Resuming(object sender, object e)
        {
            DebugWrite.MethodName(nameof(App));
            AppLevelService.Instance.Start();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            DebugWrite.MethodName(nameof(App));
            AppLevelService.Instance.Start();

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    object navigationStateObj;
                    if (ApplicationData.Current.LocalSettings.Values.TryGetValue(NAVIGATION_STATE, out navigationStateObj)
                        && navigationStateObj != null)
                    {
                        string navigationState = navigationStateObj.ToString();
                        rootFrame.SetNavigationState(navigationState);
                    }

                    object appLevelServiceState;
                    int appLevelServiceSecondsAppInUse;
                    if (ApplicationData.Current.LocalSettings.Values.TryGetValue(APP_LEVEL_SERVICE_STATE, out appLevelServiceState)
                        && appLevelServiceState != null
                        && int.TryParse(appLevelServiceState.ToString(), out appLevelServiceSecondsAppInUse))
                    {
                        AppLevelService.Instance.SecondsAppInUse = appLevelServiceSecondsAppInUse;
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                    {
                        throw new Exception("Failed to create initial page");
                    }
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            DebugWrite.MethodName(nameof(App));
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            DebugWrite.MethodName(nameof(App));
            var deferral = e.SuspendingOperation.GetDeferral();

            AppLevelService.Instance.Stop();
            ApplicationData.Current.LocalSettings.Values[APP_LEVEL_SERVICE_STATE] =
                AppLevelService.Instance.SecondsAppInUse;

            Debug.WriteLine(ApplicationData.Current.LocalSettings.Values[APP_LEVEL_SERVICE_STATE]);

            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null)
            {
                ApplicationData.Current.LocalSettings.Values[NAVIGATION_STATE] = rootFrame.GetNavigationState();
            }

            deferral.Complete();
        }
    }
}
