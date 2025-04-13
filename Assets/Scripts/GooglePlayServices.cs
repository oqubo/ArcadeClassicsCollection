using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayServices : MonoBehaviour {



	void Start ()
	{

		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			// enables saving game progress.
			//.EnableSavedGames()
			// registers a callback to handle game invitations received while the game is not running.
			//.WithInvitationDelegate(<callback method>)
			// registers a callback for turn based match notifications received while the
			// game is not running.
			//.WithMatchDelegate(<callback method>)
			// requests the email address of the player be available.
			// Will bring up a prompt for consent.
			//.RequestEmail()
			// requests a server auth code be generated so it can be passed to an
			//  associated back end server application and exchanged for an OAuth token.
			.RequestServerAuthCode(false)
			// requests an ID token be generated.  This OAuth token can be used to
			//  identify the player to other services such as Firebase.
			.RequestIdToken()
			.Build();

		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

		Social.localUser.Authenticate((bool success) => {
			if(success) print("ok");
			else print("no");
		});

		OnAddScoreToLeaderBoard();



	}

	public void OnShowLeaderBoardSnake ()
	{
		//Social.ShowLeaderboardUI (); // Show all leaderboard
		PlayGamesPlatform.Instance.ShowLeaderboardUI (GPGSIds.leaderboard_snake); // Show current (Active) leaderboard
	}

	public void OnShowLeaderBoardArkanoid ()
	{
		//Social.ShowLeaderboardUI (); // Show all leaderboard
		PlayGamesPlatform.Instance.ShowLeaderboardUI (GPGSIds.leaderboard_arkanoid); // Show current (Active) leaderboard
	}

	public void OnShowLeaderBoardPacman ()
	{
		//Social.ShowLeaderboardUI (); // Show all leaderboard
		PlayGamesPlatform.Instance.ShowLeaderboardUI (GPGSIds.leaderboard_pacman); // Show current (Active) leaderboard
	}

	public void OnAddScoreToLeaderBoard ()
	{
		if (Social.localUser.authenticated) {


			Social.ReportScore (PlayerPrefs.GetInt ("puntosSnake"), GPGSIds.leaderboard_snake, (bool success) =>
				{
					if (success) { Debug.Log ("Update Score Success");} 
					else { Debug.Log ("Update Score Fail");}
				});
			Social.ReportScore (PlayerPrefs.GetInt ("puntosArkanoid"), GPGSIds.leaderboard_arkanoid, (bool success) =>
				{
					if (success) { Debug.Log ("Update Score Success");} 
					else { Debug.Log ("Update Score Fail");}
				});			
			Social.ReportScore (PlayerPrefs.GetInt ("puntosPacman"), GPGSIds.leaderboard_pacman, (bool success) =>
				{
					if (success) { Debug.Log ("Update Score Success");} 
					else { Debug.Log ("Update Score Fail");}
				});
		}
	}



}


