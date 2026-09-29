using UnityEngine;

public class ArenaWall : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        // when the player run outside of the safezone.
        if(col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Cannot leave arena");
        }
    }
}