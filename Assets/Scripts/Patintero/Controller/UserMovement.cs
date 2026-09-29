using UnityEngine;
using UnityEngine.InputSystem;

public class UserMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;

    [Header("Stamina")]
    public float maxStamina = 5f;
    public float stamina;
    public float staminaRegenSpeed = 1f;
    public float regenDelay = 2f;
    private float regenTimer;

    [Header("References")]
    public CharacterController controller;
    public Animator animator;
    public InputActionReference moveAction;

    private bool isRunning;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        stamina = maxStamina;
    }

    void OnEnable() => moveAction.action.Enable();
    void OnDisable() => moveAction.action.Disable();

    void Update()
    {
        MovePlayer();
        HandleStamina();
    }

    void MovePlayer()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        Transform cam = Camera.main.transform;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * move.z + right * move.x;

        float speed = isRunning && stamina > 0 ? runSpeed : walkSpeed;

        if (isRunning && stamina > 0)
        {
            stamina -= Time.deltaTime;
            regenTimer = regenDelay;
        }

        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                10f * Time.deltaTime
            );
        }

        controller.Move(direction * speed * Time.deltaTime);

        // Animation
        animator.SetFloat("Speed", direction.magnitude > 0.1f ? (isRunning ? 1f : 0.5f) : 0f);
    }

    void HandleStamina()
    {
        if (!isRunning && stamina < maxStamina)
        {
            if (regenTimer > 0)
                regenTimer -= Time.deltaTime;
            else
                stamina += staminaRegenSpeed * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    public void StartRunning()
    {
        if (stamina > 0)
            isRunning = true;
    }

    public void StopRunning()
    {
        isRunning = false;
    }
}