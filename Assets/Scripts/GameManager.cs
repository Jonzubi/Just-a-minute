using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameManager : MonoBehaviour
{
    public static GameManager _gameManager;
    ScreenIdentifier[] _screenIdentifiers;
    RecordLoader[] _recordLoaders;
    ResultText _resultText;
    Modes _mode;
    Toast _toast;
    BeforePlayCounter _beforePlayCounter;

    // PlayGame variables
    double _timeStarted = 0;
    bool _gameStarted = false;

    private void Awake() {
        Application.targetFrameRate = 60;

        _toast = FindObjectOfType<Toast>();
        _screenIdentifiers = FindObjectsOfType<ScreenIdentifier>();
        _recordLoaders = FindObjectsOfType<RecordLoader>();
        _resultText = FindObjectOfType<ResultText>();
        _beforePlayCounter = FindObjectOfType<BeforePlayCounter>();

        _toast.gameObject.SetActive(false);
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

    public Modes Mode {
        get {
            return _mode;
        }
        set {
            _mode = value;
        }
    }

    public void PlayGame() {
        _gameStarted = true;
        _timeStarted = 0;
        SetActive(EScreenIdentifier.PLAY);
        _resultText.DissapearRecordText();
    }

    public void BeforePlayGame() {
        SetActive(EScreenIdentifier.BEFORE_PLAY);
        _beforePlayCounter.StartCounter();
    }

    public void JustAMinute() {
        _gameStarted = false;
        SetActive(EScreenIdentifier.RESULTS);
        _resultText.SetResult(_timeStarted);

        foreach (var record in _recordLoaders) {
            record.ReloadRecords();
        }
    }

    public void ShowToast(string text) {
        _toast.Show(text);
    }
}
