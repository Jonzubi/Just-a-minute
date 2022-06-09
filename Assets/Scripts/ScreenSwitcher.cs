using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GooglePlayGames;

public class ScreenSwitcher : MonoBehaviour, IPointerClickHandler
{
    public EScreenIdentifier screenIndex;

    GameManager _gameManager;
    GooglePlayServices _googlePlayServices;
    private void Awake() {
        _gameManager = GameManager.GetInstance;
        _googlePlayServices = GooglePlayServices.Instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (screenIndex == EScreenIdentifier.SCORES) {
            if (!_googlePlayServices.ImConnected) {
                _gameManager.ShowToast("You are not connected to Google Play, try later");
                return;
            }
            Social.ShowLeaderboardUI();
            return;
        }
        _gameManager.SetActive(screenIndex);
    }
}
