using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
		}

		private void PartA_Click(object sender, RoutedEventArgs routedEventArgs)
		{
			Navigate("A");
		}

		private void PartB_Click(object sender, RoutedEventArgs routedEventArgs)
		{
			Navigate("B");
		}

		private void PartC_Click(object sender, RoutedEventArgs routedEventArgs)
		{
			Navigate("C");
		}

		private void PartD_Click(object sender, RoutedEventArgs routedEventArgs)
		{
			Navigate("D");
		}

		private void PartE_Click(object sender, RoutedEventArgs routedEventArgs)
		{
			Navigate("E");
		}

		private void Navigate(string stateParameter)
		{
			//TODO: AppLifecycleDemo 1.0 - Navigate

			Frame.Navigate(typeof(ContentPage), stateParameter);
		}
	}
}
