using UnityEngine;
using TMPro;

public class ResultText : MonoBehaviour
{
    double _time, _bestTime;

    private void Start() {
        _bestTime = Utilities.GetDouble("BestTime");     
    }

    public void SetResult(double time)
    {
        _time = time;
        double difference = 60 - _time;
        double bestDifference = 60 - _bestTime;
        if (difference < bestDifference) {
            Utilities.SetDouble("BestTime", _time);
            _bestTime = _time;
        }
        GetComponent<TextMeshProUGUI>().text = $"Your time: {_time}s\n\nBest time: {_bestTime}s";
    }
}
