using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AppLifecycleDemo.Uwp.Views
{
	public sealed partial class ContentPage : Page
	{
		const string HAS_SAVED_NOTES = "HAS_SAVED_NOTES";
		const string NOTES_A_KEY = "NOTES_A_KEY";
		const string NOTES_B_KEY = "NOTES_B_KEY";
		const string NOTES_C_KEY = "NOTES_C_KEY";
		const string NOTES_D_KEY = "NOTES_D_KEY";
		const string NOTES_E_KEY = "NOTES_E_KEY";

		public ContentPage()
		{
			InitializeComponent();
		}

        protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			//TODO: AppLifecycleDemo 2.0 - OnNavigatedTo

			base.OnNavigatedTo(e);

			string stateName = e.Parameter != null ? e.Parameter.ToString() : "A";
			StateName.Text = stateName;

			switch (stateName)
			{
				case "A":
					StateA.Visibility = Visibility.Visible;
					break;
				case "B":
					StateB.Visibility = Visibility.Visible;
					break;
				case "C":
					StateC.Visibility = Visibility.Visible;
					break;
				case "D":
					StateD.Visibility = Visibility.Visible;
					break;
				case "E":
					StateE.Visibility = Visibility.Visible;
					break;
			}

			//TODO: AppLifecycleDemo 4.0 - Restore State

			bool retrieveSavedNotes = ApplicationData.Current.LocalSettings.Values.Keys.Contains(HAS_SAVED_NOTES);
			if (retrieveSavedNotes)
			{
				NotesA.Text = (string)ApplicationData.Current.LocalSettings.Values[NOTES_A_KEY];
				NotesB.Text = (string)ApplicationData.Current.LocalSettings.Values[NOTES_B_KEY];
				NotesC.Text = (string)ApplicationData.Current.LocalSettings.Values[NOTES_C_KEY];
				NotesD.Text = (string)ApplicationData.Current.LocalSettings.Values[NOTES_D_KEY];
				NotesE.Text = (string)ApplicationData.Current.LocalSettings.Values[NOTES_E_KEY];
			}
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			//TODO: AppLifecycleDemo 3.0 - Save State

			base.OnNavigatedFrom(e);

			ApplicationData.Current.LocalSettings.Values[HAS_SAVED_NOTES] = true;
			ApplicationData.Current.LocalSettings.Values[NOTES_A_KEY] = NotesA.Text;
			ApplicationData.Current.LocalSettings.Values[NOTES_B_KEY] = NotesB.Text;
			ApplicationData.Current.LocalSettings.Values[NOTES_C_KEY] = NotesC.Text;
			ApplicationData.Current.LocalSettings.Values[NOTES_D_KEY] = NotesD.Text;
			ApplicationData.Current.LocalSettings.Values[NOTES_E_KEY] = NotesE.Text;
		}

        private void BackButtonClick( object sender, RoutedEventArgs e )
        {
            Frame.GoBack();
        }
    }
}
