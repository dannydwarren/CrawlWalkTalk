using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AppLifecycleDemo.Uwp.Services;

namespace AppLifecycleDemo.Uwp.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = AppLevelService.Instance;
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

            Frame.Navigate(typeof(ContentPage), stateParameter);
        }
    }
}
