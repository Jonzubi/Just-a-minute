using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public static GameManager _gameManager;
    ScreenIdentifier[] _screenIdentifiers;
    ResultText _resultText;

    // PlayGame variables
    double _timeStarted = 0;
    bool _gameStarted = false;

    private void Awake() {
        _screenIdentifiers = FindObjectsOfType<ScreenIdentifier>();
        _resultText = FindObjectOfType<ResultText>();
    }

    private void Start() {
        SetActive(0);
    }

    private void Update() {
        if (_gameStarted) 
            _timeStarted += Time.deltaTime;
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

    public void PlayGame() {
        _gameStarted = true;
        _timeStarted = 0;
    }

    public void JustAMinute() {
        _gameStarted = false;
        SetActive(EScreenIdentifier.RESULTS);
        _resultText.SetResult(_timeStarted);
        Debug.Log("Time since start: " + _timeStarted);
    }
}
