using UnityEngine;
using UnityEngine.EventSystems;

public class SprintInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UserMovement player;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.StartRunning();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.StopRunning();
    }
}