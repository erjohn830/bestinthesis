using UnityEngine; // allows access to Unity functions

public class PlayerTouchSystem : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // it will check if the object touched the runner player.
        if (hit.collider.CompareTag("Runner"))
        {
            // get RunnerAI script from the runner object.
            RunnerAI runner = hit.collider.GetComponent<RunnerAI>();

            if (runner != null)
            {
                // call freeze the runner.
                runner.Freeze();
            }
        }
    }
}