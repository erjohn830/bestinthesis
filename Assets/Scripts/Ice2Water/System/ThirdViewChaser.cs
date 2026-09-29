using UnityEngine;

public class ThirdViewChaser : MonoBehaviour
{
    public Transform player;

    public float distance = 6f;
    public float height = 4f;

    public float smoothSpeed = 2f;

    void LateUpdate()
    {
        if (player == null) return;
        Vector3 target = player.position - player.forward * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, target, smoothSpeed * Time.deltaTime);
        transform.LookAt(player);
    }
}