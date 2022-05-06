using UnityEngine;
using UnityEngine.EventSystems;

public class JustAMinute : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.GetInstance.JustAMinute();
    }
}
