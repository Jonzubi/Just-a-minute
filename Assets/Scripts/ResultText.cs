using UnityEngine;
using System;
using TMPro;

public class ResultText : MonoBehaviour
{
    double _time, _bestTime;
    [SerializeField] GameObject recordText;

    private void Awake() {
        recordText.SetActive(false);
    }

    public void DissapearRecordText() {
        recordText.SetActive(false);
    }

    public void SetResult(double time)
    {
        string recordID = $"BestTime_{GameManager.GetInstance.Mode.ToString()}";
        int targetSecs = (int)GameManager.GetInstance.Mode;
        _bestTime = Utilities.GetDouble(recordID);
        _time = time;
        double difference = Math.Abs(targetSecs - _time);
        double bestDifference = Math.Abs(targetSecs - _bestTime);
        if (difference < bestDifference) {
            recordText.SetActive(true);
            GooglePlayServices.Instance.PostRecord(difference);
            Utilities.SetDouble(recordID, _time);
            _bestTime = _time;
        }
        GetComponent<TextMeshProUGUI>().text = $"Your time: {_time}s\n\nBest time: {_bestTime}s";
    }
}
