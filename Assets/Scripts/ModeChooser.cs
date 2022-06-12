using UnityEngine;
using UnityEngine.EventSystems;

public class ModeChooser : MonoBehaviour, IPointerClickHandler
{
    public Modes mode;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.GetInstance.Mode = mode;
        GameManager.GetInstance.BeforePlayGame();
    }
}
