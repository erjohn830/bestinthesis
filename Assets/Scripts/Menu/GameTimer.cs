using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeDuration = 30f;
    private float currentTime;

    [Header("UI")]
    public TMP_Text timerText;

    [Header("Hearts")]
    public GameObject[] hearts;

    [Header("Manager")]
    public LevelManager levelManager; 

    private int currentHearts;
    private bool isGameOver = false;

    void Start()
    {
        if (levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }

        ResetForNewLevel();
    }

    void Update()
    {
        if (isGameOver) return;

        RunTimer();
        UpdateUI();
    }

    void RunTimer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            LoseHeart();
            ResetTimer();
        }
    }

    public void ResetTimer()
    {
        currentTime = timeDuration;
    }

    public void ResetForNewLevel()
    {
        currentTime = timeDuration;
        currentHearts = hearts.Length;
        isGameOver = false;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(true);
        }

        Time.timeScale = 1f;
    }

    void LoseHeart()
    {
        if (currentHearts > 0)
        {
            currentHearts--;
            hearts[currentHearts].SetActive(false);

            if (currentHearts == 0)
            {
                GameOver();
            }
        }
    }

    void UpdateUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(currentTime).ToString();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");

        isGameOver = true;

        if (levelManager != null)
        {
            levelManager.LoseGame(); // ✅ USE LEVEL MANAGER
        }
    }
}