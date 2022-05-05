using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager _gameManager;
    ScreenIdentifier[] _screenIdentifiers;

    private void Awake() {
        _screenIdentifiers = FindObjectsOfType<ScreenIdentifier>();
    }

    private void Start() {
        SetActive(0);
    }
    public static GameManager GetInstance {
        get {
            if (_gameManager == null) {
                _gameManager = FindObjectOfType<GameManager>();
            }
            return _gameManager;
        }
    }

    public void SetActive(EScreenIdentifier index)
    {
        foreach (ScreenIdentifier screenIdentifier in _screenIdentifiers)
        {
            screenIdentifier.SetActive(index);
        }
    }
}
