using UnityEngine;
using System.Collections;

public class JumpJudge : MonoBehaviour
{
    public Transform perfectSpot;
    public Transform spawnPoint;

    public PlayerController player;
    public LifeSystem life;
    public LevelManager levelManager;

    public void PerfectJump()
    {
        StartCoroutine(PerfectRoutine());
    }

    public void BadJump()
    {
        StartCoroutine(BadJumpRoutine());
    }

    IEnumerator PerfectRoutine()
    {
        // STOP running first
        player.StopPlayer();
        player.PerformJump();

        float dynamicHeight = 5f + (levelManager.currentLevel - 1) * 1.6f;
        yield return player.StartCoroutine(
            player.JumpToPosition(perfectSpot.position, dynamicHeight, 1.4f)
        );

        // this will stop the player
        player.StopPlayer();

        levelManager.AddScore(100);
        levelManager.NextLevel();

        // Proper teleport
        CharacterController cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        player.transform.position = spawnPoint.position;
        cc.enabled = true;

        yield return new WaitForSeconds(0.2f);

        player.EnableRun();
    }

    IEnumerator BadJumpRoutine()
    {
        player.StopPlayer();
        player.PerformJump();

        float dynamicHeight = 5f + (levelManager.currentLevel - 1) * 0.8f;
        yield return player.StartCoroutine(
            player.JumpToPosition(perfectSpot.position, dynamicHeight, 0.6f)
        );

        player.StopPlayer();
        life.LoseLife();

        if (life.lives > 0)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            cc.enabled = false;
            player.transform.position = spawnPoint.position;
            cc.enabled = true;

            yield return new WaitForSeconds(0.2f);

            player.EnableRun();
        }
    }
}