using System;
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
		private const string RECORD_MATCH_UNKNOWN_KEY = "RecordMatchUnknown";

		protected override void OnNavigatedTo( NavigationEventArgs e )
		{
			base.OnNavigatedTo( e );

			string player1Name = "Player One";
			string player2Name = "Player Two";
            //RecordMatch.xaml?voiceCommandName=RecordMatchWithPlayerNames&Player1Name=Danny&Player2Name=Travis
            //TODO: 3.0 - Recognize Voice Command from NavigationContext
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
				else if ( voiceCommandName == RECORD_MATCH_UNKNOWN_KEY )
				{
					PageTitle.Text = "record unknown match";
				}
			}

			InitializeMatch( player1Name, player2Name );
		}

		private async void InitializeMatch( string player1Name, string player2Name )
		{
			Player1NameTextBox.Text = player1Name;
			Player2NameTextBox.Text = player2Name;

            //TODO: 4.0 - Announce match using Text-To-Speech (TTS)
			await AnnounceMatch( player1Name, player2Name );

            //TODO: 5.0 - Use speech recognition to listen for player scores
            await UseVoiceToRecordPoints( player1Name, player2Name );
		}

		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************

        //TODO: 4.1 - Initialize SpeechSynthesizer
		private readonly SpeechSynthesizer _speechSynth = new SpeechSynthesizer();
		private async Task AnnounceMatch( string player1Name, string player2Name )
		{
            //TODO: 4.2 - Setup SpeechSynthesizer and call SpeakTextAsync() with text to be read
			await _speechSynth.SpeakTextAsync( string.Format( "Match. {0} verses {1}. Ready? Fight!", player1Name, player2Name ) );
		}

		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************
		//****************************************************************************************************

		private const string POINT_PLAYER_FORMAT = "Point {0}";
		private const string ADD_ONE_PLAYER_FORMAT = "Add One {0}";
		private const int WINNING_SCORE = 3;
		private const int WIN_BY_FACTOR = 1;

        //TODO: 5.1 - Initialize SpeechRecognizer
		private readonly SpeechRecognizer _speechReco = new SpeechRecognizer();
		private async Task UseVoiceToRecordPoints( string player1Name, string player2Name )
		{
			//Using Programmatic List Grammar
			string[] pointGrammar = new[]
				                        {
					                        string.Format(POINT_PLAYER_FORMAT, player1Name),
					                        string.Format(POINT_PLAYER_FORMAT, player2Name),
					                        string.Format(ADD_ONE_PLAYER_FORMAT, player1Name),
					                        string.Format(ADD_ONE_PLAYER_FORMAT, player2Name),
				                        };
            //TODO: 5.2 - Setup Grammars
			_speechReco.Grammars.AddGrammarFromList( "PointGrammarKey", pointGrammar );
            //TODO: 5.3 - Subscribe to AudioCaptureStateChanged in order to inform the user what state speech recognition is in
			_speechReco.AudioCaptureStateChanged += AudioCaptureStateChanged;

			int player1Points = 0;
			int player2Points = 0;
			while ( player1Points < WINNING_SCORE && player2Points < WINNING_SCORE
				   && ( player1Points > player2Points - WIN_BY_FACTOR || player2Points > player1Points - WIN_BY_FACTOR ) )
			{
                //TODO: 5.4 - Listen for result with RecognizeAsync() on the SpeechRecognizer
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
			Winner.Text = string.Format( "Winner: {0}!!!!", winnerName );
			SpeechStatus.Text = "Stopped";
		
			await _speechSynth.SpeakTextAsync( string.Format( "{0} just destroyed {1}.", player1Name, player2Name ) );
		}

		private void AudioCaptureStateChanged( SpeechRecognizer sender, SpeechRecognizerAudioCaptureStateChangedEventArgs args )
		{
            //TODO: 5.5 - Process AudioCaptureStateChanged (NOTE: This event is fired on a background thread)
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