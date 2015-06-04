using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AppLifecycleDemo.Universal.Services;

namespace AppLifecycleDemo.Universal.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = AppLevelService.Instance;

#if WINDOWS_APP
            RootGrid.ColumnDefinitions.Add(new ColumnDefinition());
            RootGrid.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.SetColumn(ActionSection, 0);
            Grid.SetColumn(SecondsSinceLaunchSection, 1);
#elif WINDOWS_PHONE_APP
            RootGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            RootGrid.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(ActionSection, 1);
            Grid.SetRow(SecondsSinceLaunchSection, 0);
            SecondsSinceLaunchHeaderTextBlock.FontSize = 40;
            SecondsInUseHeaderTextBlock.FontSize = 40;
#endif
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
