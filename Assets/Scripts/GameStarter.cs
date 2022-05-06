using UnityEngine;
using UnityEngine.EventSystems;

public class GameStarter : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.GetInstance.PlayGame();
    }
}
