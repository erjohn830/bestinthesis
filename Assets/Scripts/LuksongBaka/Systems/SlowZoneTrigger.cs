using UnityEngine;

public class SlowZoneTrigger : MonoBehaviour
{
    public TimingBar bar;
    public PlayerController player;
    public GameObject jumpButton;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.EnterSlowZone();
            bar.Activate();
            jumpButton.SetActive(true);
        }
    }
}