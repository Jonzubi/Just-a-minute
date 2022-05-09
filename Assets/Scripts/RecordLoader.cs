using UnityEngine;
using TMPro;

public class RecordLoader : MonoBehaviour
{
    public Modes mode;
    private void Start() {
        GetComponent<TextMeshProUGUI>().text = "Best time:\n" + Utilities.GetDouble($"BestTime_{mode.ToString()}").ToString("F4");
    }

    public void ReloadRecords()
    {
        GetComponent<TextMeshProUGUI>().text = "Best time:\n" + Utilities.GetDouble($"BestTime_{mode.ToString()}").ToString("F4");
    }
}
