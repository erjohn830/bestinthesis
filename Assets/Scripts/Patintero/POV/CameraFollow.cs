using UnityEngine;

public class ThirdCamView : MonoBehaviour
{
    public Transform player;

    [Header("Camera Settings")]
    public float height = 7f;
    public float distance = 10f;

    private int cameraSide = -1; // default side
    private Vector3 velocity;

    void LateUpdate()
    {
        if (player == null) return;

        //(NO ROTATION WITH PLAYER)
        Vector3 offset = new Vector3(0, height, distance * cameraSide);

        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            0.2f
        );

        transform.LookAt(player.position + Vector3.up * 1.5f);
    }

    //it will called in endzone
    public void SetThirdPersonView()
    {
        cameraSide = 1; // switch side
    }

    //it will called in EnterZone
    public void ResetToDefaultView()
    {
        cameraSide = -1; // back to original
    }
}