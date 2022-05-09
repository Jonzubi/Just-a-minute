using UnityEngine;
using System;
using TMPro;

public class ResultText : MonoBehaviour
{
    double _time, _bestTime;

    public void SetResult(double time)
    {
        string recordID = $"BestTime_{GameManager.GetInstance.Mode.ToString()}";
        int targetSecs = (int)GameManager.GetInstance.Mode;
        _bestTime = Utilities.GetDouble(recordID);
        _time = time;
        double difference = targetSecs - _time;
        double bestDifference = Math.Abs(targetSecs - _bestTime);
        if (difference < bestDifference) {
            Utilities.SetDouble(recordID, _time);
            _bestTime = _time;
        }
        GetComponent<TextMeshProUGUI>().text = $"Your time: {_time}s\n\nBest time: {_bestTime}s";
    }
}
