using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeforePlayCounter : MonoBehaviour
{
    TextMeshProUGUI _text;
    float _timeSaver = 3;
    bool _isCounting = false;

    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (_isCounting) {
            _timeSaver -= Time.deltaTime;
            _text.text = Mathf.CeilToInt(_timeSaver).ToString();
            
            float auxScale = _timeSaver - Mathf.FloorToInt(_timeSaver);
            _text.transform.localScale = new Vector3(auxScale, auxScale, auxScale);

            if (_timeSaver <= 0) {
                _isCounting = false;
                _text.transform.localScale = new Vector3(1, 1, 1);
                GameManager.GetInstance.PlayGame();
            }

        }
    }

    public void StartCounter() {
        _timeSaver = 3;
        _isCounting = true;
        _text.transform.localScale = new Vector3(1, 1, 1);
    }

    public void StopCounter() {
        _isCounting = false;
    }
}
