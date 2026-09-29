using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 6f;
    public GameObject jumpButton;

    private CharacterController controller;
    private Animator anim;

    private bool autoRun = true;
    private bool waitingForJump = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.SetBool("Run", true);
    }

    void Update()
    {
        if (autoRun)
        {
            controller.Move(transform.forward * runSpeed * Time.deltaTime);
        }
    }

    public void EnterSlowZone()
    {
        autoRun = false;
        waitingForJump = true;

        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        anim.speed = 0f;

        if (jumpButton != null)
            jumpButton.SetActive(true);
    }

    public void PerformJump()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        anim.speed = 1f;
        anim.SetTrigger("Jump");
    }

    public void EnableRun()
    {
        autoRun = true;
        anim.SetBool("Run", true);
    }

    public void StopPlayer()
    {
        autoRun = false;

        // Stop animation
        anim.SetBool("Run", false);
        anim.speed = 1f;
    }

    public void ResetToSlowState()
    {
        autoRun = false;
        waitingForJump = true;

        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        anim.speed = 0f;

        if (jumpButton != null)
            jumpButton.SetActive(true);
    }

    public IEnumerator JumpToPosition(Vector3 targetPosition, float height, float duration)
    {
        controller.enabled = false;

        Vector3 startPosition = transform.position;
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;

            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, t);
            float arc = height * 4 * (t - t * t);
            currentPos.y += arc;

            transform.position = currentPos;

            time += Time.deltaTime;
            yield return null;
        }

        controller.enabled = false;
        transform.position = targetPosition;
        controller.enabled = true;
    }
}