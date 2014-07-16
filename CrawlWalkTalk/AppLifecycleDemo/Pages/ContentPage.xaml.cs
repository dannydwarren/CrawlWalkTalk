using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace AppLifecycleDemo.Pages
{
	public partial class ContentPage : PhoneApplicationPage
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
			base.OnNavigatedTo(e);

			bool retrieveSavedNotes = PhoneApplicationService.Current.State.Keys.Contains(HAS_SAVED_NOTES);
			if (retrieveSavedNotes)
			{
				NotesA.Text = (string)PhoneApplicationService.Current.State[NOTES_A_KEY];
				NotesB.Text = (string)PhoneApplicationService.Current.State[NOTES_B_KEY];
				NotesC.Text = (string)PhoneApplicationService.Current.State[NOTES_C_KEY];
				NotesD.Text = (string)PhoneApplicationService.Current.State[NOTES_D_KEY];
				NotesE.Text = (string)PhoneApplicationService.Current.State[NOTES_E_KEY];
			}

			string stateName = NavigationContext.QueryString["state"];
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
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);

			PhoneApplicationService.Current.State[HAS_SAVED_NOTES] = true;
			PhoneApplicationService.Current.State[NOTES_A_KEY] = NotesA.Text;
			PhoneApplicationService.Current.State[NOTES_B_KEY] = NotesB.Text;
			PhoneApplicationService.Current.State[NOTES_C_KEY] = NotesC.Text;
			PhoneApplicationService.Current.State[NOTES_D_KEY] = NotesD.Text;
			PhoneApplicationService.Current.State[NOTES_E_KEY] = NotesE.Text;
		}
	}
}