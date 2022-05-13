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
    public Dictionary<string, LeaderBoardEntry> LeaderBoardsData = new Dictionary<string, LeaderBoardEntry>();

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
                _googlePlayServices = FindObjectOfType<GooglePlayServices>();
            }
            return _googlePlayServices;
        }
    }

    
    public void SignInToGooglePlay() {
        PlayGamesPlatform.Instance.Authenticate((success) => {
            if (success == SignInStatus.Success) {
                _imConnected = true;
                GameManager.GetInstance.ShowToast("Connected to Google Play");
            } else if(success == SignInStatus.Canceled) {
                GameManager.GetInstance.ShowToast("Google Play sign in canceled");
            } else {
                GameManager.GetInstance.ShowToast("Google Play sign in failed");
            }
        });
    }

    // Post record in Google Leaderboard
    public void PostRecord(double difference) {
        if (!_imConnected)
        {
            GameManager.GetInstance.ShowToast("You are not connected to Google Play");
            return;
        }
        Social.ReportScore((long)difference, GooglePlayConstants.LeaderBoardTokens[GameManager.GetInstance.Mode.ToString()], (success) => {
            if (success) {
                Debug.LogError("Post record success");
                GameManager.GetInstance.ShowToast("Record posted successfully to GooglePlay");
            } else {
                Debug.LogError("Failed to post record to GooglePlay");
                GameManager.GetInstance.ShowToast("Record posting failed to GooglePlay");
            }
        });
    }

    // Get all leaderboards data from Google Leaderboard
    public void GetLeaderboardData() {
        if (!_imConnected)
        {
            GameManager.GetInstance.ShowToast("You are not connected to Google Play");
            return;
        }
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
