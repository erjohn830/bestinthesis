using UnityEngine;
using UnityEngine.EventSystems;

public class RunButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public IceChaserController player; // reference to player

    public void OnPointerDown(PointerEventData eventData)
    {
        // when button is pressed start running
        player.StartRunning();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // when released stop running
        player.StopRunning();
    }
}