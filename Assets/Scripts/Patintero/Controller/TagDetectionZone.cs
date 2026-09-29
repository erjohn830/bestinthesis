using UnityEngine;

public class TagDetectionZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameFlowManager.instance.PlayerTagged();
    }
}