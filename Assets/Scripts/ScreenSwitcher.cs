using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenSwitcher : MonoBehaviour, IPointerClickHandler
{
    public EScreenIdentifier screenIndex;

    GameManager _gameManager;
    private void Awake() {
        _gameManager = GameManager.GetInstance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _gameManager.SetActive(screenIndex);
    }
}
