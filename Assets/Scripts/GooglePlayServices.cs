using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayServices : MonoBehaviour
{
    public class LeaderBoardEntry
    {
        public string userId;
        public long score;
    }

    public static GooglePlayServices _googlePlayServices;
    GooglePlayStatus _googlePlayStatus;
    Dictionary<string, LeaderBoardEntry> LeaderBoardsData = new Dictionary<string, LeaderBoardEntry>();

    bool _imConnected = false;

    private void Awake() {
        _googlePlayStatus = FindObjectOfType<GooglePlayStatus>();

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    private void Start() {
        SignInToGooglePlay();
    }

    public static GooglePlayServices Instance {
        get {
            if (_googlePlayServices == null) {
                _googlePlayServices = new GooglePlayServices();
            }
            return _googlePlayServices;
        }
    }

    
    public void SignInToGooglePlay() {
        PlayGamesPlatform.Instance.Authenticate((success) => {
            if (success == SignInStatus.Success) {
                _imConnected = true;
                _googlePlayStatus.SetStatusText("Connected to Google Play");
            } else if(success == SignInStatus.Canceled) {
                _googlePlayStatus.SetStatusText("Google Play sign is canceled");
            } else {
                _googlePlayStatus.SetStatusText("Google Play sign is failed");
            }
        });
    }

    // Post record in Google Leaderboard
    public void PostRecord(double difference) {
        if (!_imConnected)
            return;
        Social.ReportScore((long)difference, GooglePlayConstants.LeaderBoardTokens[GameManager.GetInstance.Mode.ToString()], (success) => {
            if (success) {
                Debug.Log("Record posted successfully");
            } else {
                Debug.Log("Record post failed");
            }
        });
    }

    // Gest all leaderboards data from Google Leaderboard
    public void GetLeaderboardData() {
        if (!_imConnected)
            return;
        Dictionary<string, string>.KeyCollection leaderBoardKeys = GooglePlayConstants.LeaderBoardTokens.Keys;
        foreach (string key in leaderBoardKeys) {
            Social.LoadScores(key, (scores) => {
                foreach (var score in scores)
                {
                    LeaderBoardsData[key] = new LeaderBoardEntry { userId = score.userID, score = score.value };
                }
            });
        }
    }
}
