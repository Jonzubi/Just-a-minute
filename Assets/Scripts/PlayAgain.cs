using UnityEngine;
using UnityEngine.EventSystems;
public class PlayAgain : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.GetInstance.BeforePlayGame();
    }   
}
