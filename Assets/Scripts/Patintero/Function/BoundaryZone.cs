using UnityEngine;

public class BoundaryZone : MonoBehaviour
{
    public Transform resetPoint;

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = resetPoint.position;
        }
    }
}