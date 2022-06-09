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
    public Dictionary<string, List<LeaderBoardEntry>> LeaderBoardsData = new Dictionary<string, List<LeaderBoardEntry>>();

    bool _imConnected = false;
    float factorDifferenciaPuntos = 1000000;
    const float AuthenticationWaitTimeSeconds = 10;

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
                return;
            }            
            StartCoroutine(WaitForAuthentication());
        });
    }

    private IEnumerator WaitForAuthentication()
    {
        var startTime = Time.realtimeSinceStartup;

        while (!Social.localUser.authenticated)
        {
            if (Time.realtimeSinceStartup - startTime > AuthenticationWaitTimeSeconds)
            {
                // X seconds have passed and we are still not authenticated, time to give up.
                break;
            }

            yield return null;
        }

        if (Social.localUser.authenticated)
        {
            _imConnected = true;
            GameManager.GetInstance.ShowToast("Connected to Google Play");
        }
        else
        {
            GameManager.GetInstance.ShowToast("Google Play sign in failed");
        }
    }

    // Post record in Google Leaderboard
    public void PostRecord(double difference) {
        if (!_imConnected)
        {
            GameManager.GetInstance.ShowToast("You are not connected to Google Play");
            return;
        }
        long googlePoints = PasteDifferenceToGooglePlayPoints(difference);
        Social.ReportScore(googlePoints, GooglePlayConstants.LeaderBoardTokens[GameManager.GetInstance.Mode.ToString()], (success) => {
            if (success) {
                GameManager.GetInstance.ShowToast("Record posted successfully to GooglePlay");
            } else {
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
        InitializeLeaderBoardData();
        foreach (string key in leaderBoardKeys) {
            Social.LoadScores(key, (scores) => {
                foreach (var score in scores)
                {
                    LeaderBoardsData[key].Add(new LeaderBoardEntry { userId = score.userID, score = score.value });
                }
            });
        }
    }

    void InitializeLeaderBoardData()
    {
        LeaderBoardsData = new Dictionary<string, List<LeaderBoardEntry>>();
        foreach (var key in GooglePlayConstants.LeaderBoardTokens.Keys)
        {
            LeaderBoardsData[key] = new List<LeaderBoardEntry>();
        }
    }

    public long PasteDifferenceToGooglePlayPoints(double difference)
    {
        long maxDifference = (long)((int)GameManager.GetInstance.Mode * factorDifferenciaPuntos);
        long googlePoints = (long)(difference * factorDifferenciaPuntos);
        googlePoints = maxDifference - googlePoints;
        return googlePoints;
    }
    
    public bool ImConnected {
        get {
            return _imConnected;
        }
    }
}
