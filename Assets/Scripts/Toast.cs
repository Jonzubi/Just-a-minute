using UnityEngine;
using TMPro;
using System.Collections;

public class Toast : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Awake() {
        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void Show(string message) {
        gameObject.SetActive(true);
        _text.text = message;
        StartCoroutine(Hide());
    }

    IEnumerator Hide() {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
