using UnityEngine;

public class EndZone : MonoBehaviour
{
    public ThirdCamView cam;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Change state
        GameFlowManager.instance.playerState = GameFlowManager.PlayerState.Returning;

        // Turn player around
        other.transform.forward *= -1;

        // Switch camera
        if (cam != null)
            cam.SetThirdPersonView();

        // Make enemies react
        EnemyPatrolController[] enemies = FindObjectsOfType<EnemyPatrolController>();

        foreach (EnemyPatrolController enemy in enemies)
        {
            enemy.EnterReturnPhase(); 
        }
    }
}