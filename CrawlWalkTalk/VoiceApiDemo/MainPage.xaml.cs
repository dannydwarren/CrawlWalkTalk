using System;
using System.Windows;
using Windows.Phone.Speech.VoiceCommands;
using Microsoft.Phone.Controls;

namespace VoiceApiDemo
{
	public partial class MainPage : PhoneApplicationPage
	{
		public MainPage()
		{
			InitializeComponent();

			RegisterVoiceCommands();
		}

		private async void RegisterVoiceCommands()
		{
			//TODO: VoiceApiDemo 2.0 - Install VCD File & Update Phrase Lists
			try
			{
				await VoiceCommandService.InstallCommandSetsFromFileAsync(
					new Uri( "ms-appx:///DAPPrVCDs.xml" ) );

				VoiceCommandSet vcs = null;
				if ( VoiceCommandService.InstalledCommandSets
					.TryGetValue( "DAPPrCommandSet", out vcs ) )
				{
					//NOTE: Get from service, local data, or other source.
					string[] playerNames = new[] { "Danny", "Mike", "Scott", "Travis" };
					string[] teamNames = new[] { "Lumberjacks", "Not A Thing", "Jokers" };


					await vcs.UpdatePhraseListAsync( "Player1Name", playerNames );
					await vcs.UpdatePhraseListAsync( "Player2Name", playerNames );
					await vcs.UpdatePhraseListAsync( "Team1Name", teamNames );
					await vcs.UpdatePhraseListAsync( "Team2Name", teamNames );
				}
			}
			catch ( Exception e )
			{
				//NOTE: Error Codes http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj662934(v=vs.105).aspx
				MessageBox.Show( e.Message, "Failed to Register Voice Commands", MessageBoxButton.OK );
			}

		}

		private void RecordMatch_Click( object sender, RoutedEventArgs e )
		{
			NavigationService.Navigate( new Uri( "/RecordMatch.xaml", UriKind.Relative ) );
		}

		private void Standings_Click( object sender, RoutedEventArgs e )
		{
			NavigationService.Navigate( new Uri( "/Standings.xaml", UriKind.Relative ) );
		}
	}
}