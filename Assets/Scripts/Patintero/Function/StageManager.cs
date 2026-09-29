using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    public int level = 1;
    public TextMeshProUGUI levelText;
    public GameObject winPanel;
    public GameObject[] levelLayouts;
    public Transform[] lobbySpawns;
    public GameObject player;

    void Start()
    {
        UpdateLevel();
    }

    //if player succeed in any level.
    public void NextLevel()
    {
        level++;

        if (level > levelLayouts.Length)
        {
            if (winPanel != null)
                winPanel.SetActive(true);

            Time.timeScale = 0f;
            return;
        }

        UpdateLevel();
    }

    void UpdateLevel()
    {
        levelText.text = "LEVEL " + level;

        for (int i = 0; i < levelLayouts.Length; i++)
        {
            levelLayouts[i].SetActive(i == level - 1);
        }

        TeleportPlayer();
    }

    void TeleportPlayer()
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        cc.enabled = false;
        player.transform.position = lobbySpawns[level - 1].position;
        cc.enabled = true;

        // Reset all enemies at start of level
        EnemyPatrolController[] enemies = FindObjectsOfType<EnemyPatrolController>();

        foreach (var enemy in enemies)
        {
            enemy.ResetEnemy();
        }
    }
}