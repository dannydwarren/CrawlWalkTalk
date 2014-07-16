using System;
using System.Threading.Tasks;
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
            //TODO: 0.0 - Enable Speech Recognition Service in Phone Simulator Settings
            //TODO: 0.1 - Enable Speech in WMAppManifest -> Capabilities
            //TODO: 0.2 - Install Voice Commands
            //NOTE: Some Ideas - 
            //	Idea 1 - Install Voice Commands each time the app loads
            //		Benefits - Always up to date
            //	Idea 2 - Use a persisted flag value to indicate whether or not to Install Voice Commands
            //		Benefits - For commands with phrase lists that never change this is more efficient
            try
            {
				//TODO: 1.0 - Review DAPPrVCDs.xml
                await VoiceCommandService.InstallCommandSetsFromFileAsync(
                    new Uri("ms-appx:///DAPPrVCDs.xml"));
                //TODO: 2.0 - Programatically update Phrase Lists
                await UpdatePhraseLists();
            }
            catch (Exception e)
            {
                //TODO: 2.1 - React to failures correctly (NOTE: This is not done correctly! LOVE DEMO CODE!)
                //NOTE: Error Codes http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj662934(v=vs.105).aspx
                MessageBox.Show(e.Message, "Failed to Register Voice Commands", MessageBoxButton.OK);
            }

        }

        private static async Task UpdatePhraseLists()
        {
            VoiceCommandSet vcs = null;
            if (VoiceCommandService.InstalledCommandSets
                .TryGetValue("DAPPrCommandSet", out vcs))
            {
                //NOTE: Get from service, local data, or other source.
                string[] playerNames = new[] { "Danny", "Mike", "Scott", "Travis" };
                string[] teamNames = new[] { "Lumberjacks", "Not A Thing", "Jokers" };


                await vcs.UpdatePhraseListAsync("Player1Name", playerNames);
                await vcs.UpdatePhraseListAsync("Player2Name", playerNames);
                await vcs.UpdatePhraseListAsync("Team1Name", teamNames);
                await vcs.UpdatePhraseListAsync("Team2Name", teamNames);
            }
        }

        private void RecordMatch_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RecordMatch.xaml", UriKind.Relative));
        }

        private void Standings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Standings.xaml", UriKind.Relative));
        }
    }
}