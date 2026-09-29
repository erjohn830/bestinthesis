using UnityEngine;                  
using System.Collections;           
using System.Collections.Generic;   
using TMPro;                        

public class GameFlow : MonoBehaviour
{
    public int currentLevel = 1;    
    public int maxLevel = 4;        

    public Transform player;       

    public GameObject runnerPrefab; 

    public Transform[] runnerSpawnPoints; 
    public Transform[] playerSpawnPoints; 

    public TMP_Text levelText;      

    List<RunnerAI> runners = new List<RunnerAI>(); 

    int frozenCount = 0;            

    void Start()
    {
        // first level when the game start.
        StartLevel();               
    }

    // this function starts a new level
    void StartLevel()
    {
        frozenCount = 0;
        ClearRunners();

        levelText.text = "LEVEL " + currentLevel;

        TeleportPlayer(playerSpawnPoints[currentLevel - 1]);

        int runnerCount = currentLevel;

        for (int i = 0; i < runnerCount; i++)
        {
            GameObject r = Instantiate(
                runnerPrefab,
                runnerSpawnPoints[i].position,
                Quaternion.identity
            );

            RunnerAI ai = r.GetComponent<RunnerAI>();

            ai.player = player;

            runners.Add(ai);
        }
        GameCountdown timer = FindObjectOfType<GameCountdown>();
        if (timer != null)
        {
            timer.SetTimeByLevel(currentLevel); // start countdown
        }
    }

    public void RunnerFrozen()
    {
        // this increase frozen runner counter
        frozenCount++;

        int runnerCount = currentLevel;

        // check if all runners are frozen.
        if (frozenCount >= runnerCount)
        {
            // start next level after delay.
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1.5f);

        currentLevel++;

        // check if player finished the last level.
        if (currentLevel > maxLevel)
        {
            Debug.Log("YOU WIN!"); 
            yield break;           
        }

        StartLevel(); 
    }

    void TeleportPlayer(Transform spawn)
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        // disable controller so teleport doesn't break.
        if (cc != null)
            cc.enabled = false;

        player.position = spawn.position; 

        // re-enable controller.
        if (cc != null)
            cc.enabled = true;
    }


    void ClearRunners()
    {
        foreach (RunnerAI r in runners)
        {
            if (r != null)
                Destroy(r.gameObject); 
        }

        runners.Clear(); 
    }
}