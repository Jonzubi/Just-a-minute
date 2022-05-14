using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsService : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static UnityAdsService _unityAdsService;
    string androidId = "4754299";
    public bool testmode = true;
    string adId = "Interstitial_Android";
    int partidasSinAd = 0;
    int showAdCada = 2;

    private void Start() {
        Advertisement.Initialize(androidId, testmode);
        LoadAd();
    }

    public static UnityAdsService GetInstance {
        get {
            if (_unityAdsService == null) {
                _unityAdsService = FindObjectOfType<UnityAdsService>();
            }
            return _unityAdsService;
        }
    }

    public void LoadAd()
    {
        Advertisement.Load(adId, this);
    }

    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Advertisement.Show(adId, this);
    }

    public void HandleAd()
    {
        if (partidasSinAd == showAdCada) {
            partidasSinAd = 0;
            ShowAd();
            LoadAd();
        } else {
            partidasSinAd++;
        }
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }
 
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }
}
