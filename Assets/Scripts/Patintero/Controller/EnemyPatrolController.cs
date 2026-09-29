using UnityEngine;
using System.Collections;

public class EnemyPatrolController : MonoBehaviour
{
    [Header("Lane Limits")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Speed")]
    public float normalSpeed = 3f;
    public float fastSpeed = 6f;
    private float currentSpeed;

    [Header("Tracking")]
    public Transform player;
    public float reactionDelay = 0.5f;

    [Header("Smooth")]
    public float smoothTime = 0.2f;

    [Header("Rotation")]
    public float rotationSpeed = 5f; 

    private float delayTimer;
    private float targetX;
    private float velocity;
    private bool isReturningPhase = false;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        currentSpeed = normalSpeed;
        targetX = transform.position.x;
    }

    void Update()
    {
        if (player == null) return;

        delayTimer -= Time.deltaTime;

        if (delayTimer <= 0f)
        {
            targetX = player.position.x;

            // Small human-like mistake
            if (Random.value < 0.2f)
                targetX += Random.Range(-1f, 1f);

            targetX = Mathf.Clamp(targetX, leftPoint.position.x, rightPoint.position.x);
            delayTimer = reactionDelay;
        }

        // rotation of taya.
        if (isReturningPhase)
        {
            RotateTowardsPlayer(); // face player
        }
        else
        {
            // Face forward normally
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, 180, 0),
                10f * Time.deltaTime
            );
        }

        MoveSmooth();
    }

    void MoveSmooth()
    {
        float newX = Mathf.SmoothDamp(
            transform.position.x,
            targetX,
            ref velocity,
            smoothTime,
            currentSpeed
        );

        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }

    //Rotate toward player
    void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f; // keep flat rotation

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    //Called when player reaches EndZone
    public void EnterReturnPhase()
    {
        isReturningPhase = true;
        currentSpeed = fastSpeed;
    }

    //Reset if needed
    public void ResetEnemy()
    {
        isReturningPhase = false;
        currentSpeed = normalSpeed;

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}