using System;
using Microsoft.Phone.Controls;

namespace AppLifecycleDemo.Pages
{
	public partial class MainPage : PhoneApplicationPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void PartA_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Navigate("A");
		}

		private void PartB_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Navigate("B");
		}

		private void PartC_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Navigate("C");
		}

		private void PartD_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Navigate("D");
		}

		private void PartE_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Navigate("E");
		}

		private void Navigate(string stateParameter)
		{
			//TODO: AppLifecycleDemo 1.0 - Navigate

			string pagePathWithParameter = string.Format("/Pages/ContentPage.xaml?state={0}", stateParameter);
			NavigationService.Navigate(new Uri(pagePathWithParameter, UriKind.Relative));
		}

	}
}