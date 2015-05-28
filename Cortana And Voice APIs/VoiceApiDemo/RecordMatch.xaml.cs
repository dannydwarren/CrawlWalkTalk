using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;
using Microsoft.Phone.Controls;

namespace VoiceApiDemo
{
	public partial class RecordMatch : PhoneApplicationPage
	{

		public RecordMatch()
		{
			InitializeComponent();
		}

		private const string VOICE_COMMAND_NAME_KEY = "voiceCommandName";
		private const string RECORD_MATCH_WITH_PLAYER_NAMES_KEY = "RecordMatchWithPlayerNames";
		private const string RECORD_MATCH_WITH_TEAM_NAMES_KEY = "RecordMatchWithTeamNames";
		private const string RECORD_MATCH_KEY = "RecordMatch";

		protected override void OnNavigatedTo( NavigationEventArgs e )
		{
			//TODO: VoiceApiDemo 3.0 - Handle Voice Navigation
			base.OnNavigatedTo( e );

			string player1Name = "Player One";
			string player2Name = "Player Two";
			string taunt = string.Empty;
			//NavigationContext.QueryString == "RecordMatch.xaml?voiceCommandName=RecordMatchWithPlayerNames&Player1Name=Danny&Player2Name=Travis&taunt=You're going down"
			string voiceCommandName;
			if ( NavigationContext.QueryString
                .TryGetValue( VOICE_COMMAND_NAME_KEY, out voiceCommandName ) )
			{
				if ( voiceCommandName == RECORD_MATCH_WITH_PLAYER_NAMES_KEY )
				{
					PageTitle.Text = "record singles match";
					player1Name = NavigationContext.QueryString["Player1Name"];
					player2Name = NavigationContext.QueryString["Player2Name"];
				}
				else if ( voiceCommandName == RECORD_MATCH_WITH_TEAM_NAMES_KEY )
				{
					PageTitle.Text = "record doubles match";
					player1Name = NavigationContext.QueryString["Team1Name"];
					player2Name = NavigationContext.QueryString["Team2Name"];
				}
				else if ( voiceCommandName == RECORD_MATCH_KEY )
				{
					PageTitle.Text = "record anonymous match";
				}

				if (NavigationContext.QueryString.Keys.Contains("taunt"))
				{
					taunt = NavigationContext.QueryString["taunt"];
				}
			}

			InitializeMatch( player1Name, player2Name, taunt );
		}

		private readonly SpeechSynthesizer _speechSynth = new SpeechSynthesizer();
		private async void InitializeMatch(string player1Name, string player2Name, string taunt)
		{
			Player1NameTextBox.Text = player1Name;
			Player2NameTextBox.Text = player2Name;

			//TODO: VoiceApiDemo 4.0 - Text to Speech
			await _speechSynth.SpeakTextAsync( string.Format( "Match. {0} verses {1}. Ready? Fight!", player1Name, player2Name ) );
			if (!string.IsNullOrEmpty(taunt))
			{
				await _speechSynth.SpeakTextAsync(taunt);
			}

            await UseVoiceToRecordPoints( player1Name, player2Name );
		}

		private const string POINT_PLAYER_FORMAT = "Point {0}";
		private const string ADD_ONE_PLAYER_FORMAT = "Add One {0}";
		private const int WINNING_SCORE = 3;
		private readonly SpeechRecognizer _speechReco = new SpeechRecognizer();

		private async Task UseVoiceToRecordPoints( string player1Name, string player2Name )
		{
			//TODO: VoiceApiDemo 5.0 - Speech to Text

			var pointGrammar = new List<string>
				                        {
					                        string.Format(POINT_PLAYER_FORMAT, player1Name),
					                        string.Format(POINT_PLAYER_FORMAT, player2Name),
					                        string.Format(ADD_ONE_PLAYER_FORMAT, player1Name),
					                        string.Format(ADD_ONE_PLAYER_FORMAT, player2Name),
				                        };

			_speechReco.Grammars.AddGrammarFromList( "PointGrammarKey", pointGrammar );

			_speechReco.AudioCaptureStateChanged += AudioCaptureStateChanged;

			int player1Points = 0;
			int player2Points = 0;
			while ( player1Points < WINNING_SCORE && player2Points < WINNING_SCORE )
			{
				SpeechRecognitionResult result = await _speechReco.RecognizeAsync();
				if ( result.Text == pointGrammar[0] || result.Text == pointGrammar[2] )
				{
					player1Points++;
				}
				else if ( result.Text == pointGrammar[1] || result.Text == pointGrammar[3] )
				{
					player2Points++;
				}

				Player1PointsTextBox.Text = player1Points.ToString();
				Player2PointsTextBox.Text = player2Points.ToString();
			}

			string winnerName = player1Points > player2Points ? player1Name : player2Name;
			string loserName = player1Points < player2Points ? player1Name : player2Name;
			Winner.Text = string.Format( "Winner: {0}!!!!", winnerName );
			SpeechStatus.Text = "Stopped";

			await _speechSynth.SpeakTextAsync(string.Format("{0} just destroyed {1}.", winnerName, loserName));
		}

		private void AudioCaptureStateChanged( SpeechRecognizer sender, SpeechRecognizerAudioCaptureStateChangedEventArgs args )
		{
			Deployment.Current.Dispatcher.BeginInvoke( () =>
			{
				switch ( args.State )
				{
					case SpeechRecognizerAudioCaptureState.Inactive:
						SpeechStatus.Text = "Processing...";
						break;
					case SpeechRecognizerAudioCaptureState.Capturing:
						SpeechStatus.Text = "Listening...";
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			} );
		}
	}
}