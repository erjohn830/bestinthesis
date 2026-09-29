using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public TimingBar bar;
    public RectTransform yellowZone;

    public GameTimer gameTimer;

    public int currentLevel = 1;
    public int maxLevel = 4;

    public float baseZoneWidth = 300f;

    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public GameObject winPanel;
    public GameObject losePanel;

    void Start()
    {
        if (gameTimer == null)
        {
            gameTimer = FindFirstObjectByType<GameTimer>(); // updated (no warning)
        }

        ApplyLevelSettings();
        UpdateScoreUI();
        UpdateLevelUI();

        if (winPanel != null)
            winPanel.SetActive(false);

        if (losePanel != null)
            losePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        currentLevel++;

        if (currentLevel > maxLevel)
        {
            WinGame();
            return;
        }

        ApplyLevelSettings();
        UpdateLevelUI();

        if (gameTimer != null)
        {
            gameTimer.ResetForNewLevel();
        }
    }

    void ApplyLevelSettings()
    {
        if (bar != null)
        {
            bar.SetLevel(currentLevel);
        }

        // Shrink yellow zone per level
        float newWidth = baseZoneWidth * Mathf.Pow(0.75f, currentLevel - 1);
        yellowZone.sizeDelta = new Vector2(newWidth, yellowZone.sizeDelta.y);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void UpdateLevelUI()
    {
        if (levelText != null)
            levelText.text = "Level " + currentLevel;
    }

    void WinGame()
    {
        Debug.Log("YOU WIN!");

        if (winPanel != null)
            winPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        Debug.Log("YOU LOSE!");

        if (losePanel != null)
            losePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}