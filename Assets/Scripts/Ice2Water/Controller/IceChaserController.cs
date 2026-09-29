using UnityEngine;
using UnityEngine.InputSystem;

public class IceChaserController : MonoBehaviour
{
    // Movement speed variables
    [Header("Movement")]
    public float walkSpeed = 4f;  
    public float runSpeed = 8f;  

    // Stamina system (limits running)
    [Header("Stamina")]
    public float maxStamina = 7f;     
    public float stamina;              
    public float staminaRegenSpeed = 1f; 
    public float regenDelay = 2f;      
    float regenTimer; 

   
    [Header("Components")]
    public CharacterController controller; 
    public Animator animator;              

    Vector3 velocity; 
    bool grounded;    

    // Input system
    [Header("Input")]
    public InputActionReference moveAction;

    bool isRunning; 

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
        grounded = controller.isGrounded;

        // this is where the player keep grounded.
        if (grounded && velocity.y < 0)
            velocity.y = -2f;

        MovePlayer();
        HandleStamina();
        ApplyGravity();
    }

    void MovePlayer()
    {
        // also movement can use joystick and keyboard.
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        Vector3 move = new Vector3(input.x, 0, input.y);

        // The camera direction
        Transform cam = Camera.main.transform;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // convert input relative to camera direction
        Vector3 direction = forward * move.z + right * move.x;

        float speed = walkSpeed;

        // the running logic.
        if (isRunning && stamina > 0)
        {
            speed = runSpeed;

            stamina -= Time.deltaTime;

            regenTimer = regenDelay;

            if (stamina <= 0)
            {
                stamina = 0;
                isRunning = false;
            }
        }

        // this is the rotation of the player to face direction.
        if (direction.magnitude > 0.1f)
        {
            Quaternion rot = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10f * Time.deltaTime);
        }

        // movement of player.
        controller.Move(direction * speed * Time.deltaTime);

        if (animator != null)
        {
            animator.SetFloat("Speed",
                direction.magnitude > 0.1f ? (isRunning ? 1f : 0.5f) : 0f);
        }
    }

    void ApplyGravity()
    {
        velocity.y += -9.81f * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleStamina()
    {
        // regenerate stamina if not running.
        if (!isRunning && stamina < maxStamina)
        {
            if (regenTimer > 0)
                regenTimer -= Time.deltaTime;
            else
                stamina += staminaRegenSpeed * Time.deltaTime;
        }
    }

    public void StartRunning()
    {
        if (stamina > 0)
            isRunning = true;
    }

    // if the runner stop running.
    public void StopRunning()
    {
        isRunning = false;
    }
}