using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager instance;

    public enum PlayerState { Lobby, Crossing, Returning }

    public PlayerState playerState = PlayerState.Lobby;
    public Transform[] lobbySpawns;
    public GameObject player;
    public PlayerHeart life;
    public GameObject gameOverPanel;

    public ThirdCamView cam;

    void Awake()
    {
        instance = this;
    }

    public void PlayerTagged()
    {
        life.LoseHeart();

        CharacterController cc = player.GetComponent<CharacterController>();
        cc.enabled = false;

        int levelIndex = FindObjectOfType<StageManager>().level - 1;
        player.transform.position = lobbySpawns[levelIndex].position;

        cc.enabled = true;

        EnemyPatrolController[] enemies = FindObjectsOfType<EnemyPatrolController>();

        foreach (var enemy in enemies)
        {
            enemy.ResetEnemy();
        }

        if (cam != null)
            cam.ResetToDefaultView();

        playerState = PlayerState.Lobby;
        
    }
    public void GameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void WinLevel()
    {
        StageManager levelManager = FindObjectOfType<StageManager>();
        levelManager.NextLevel();

        CharacterController cc = player.GetComponent<CharacterController>();
        cc.enabled = false;

        int levelIndex = levelManager.level - 1;
        player.transform.position = lobbySpawns[levelIndex].position;

        cc.enabled = true;
        playerState = PlayerState.Lobby;
    }
}