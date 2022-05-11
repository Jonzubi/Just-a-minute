using UnityEngine;
using TMPro;

public class GooglePlayStatus : MonoBehaviour
{
    TextMeshProUGUI _text;

    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
        SetStatusText("Not connected");
    }
    
    public void SetStatusText(string message)
    {
        _text.text = message;
    }
}
