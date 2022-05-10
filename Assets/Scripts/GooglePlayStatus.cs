using UnityEngine;
using TMPro;

public class GooglePlayStatus : MonoBehaviour
{
    TextMeshProUGUI _text;

    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
        SetStatusText(false);
    }
    
    public void SetStatusText(bool isSignedIn)
    {
        _text.text = isSignedIn ? "Google Play: Connected" : "Google Play: Disconnected";
    }
}
