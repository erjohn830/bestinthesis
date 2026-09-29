using UnityEngine;
using TMPro;

public class GameCountdown : MonoBehaviour
{
    public float timeLeft;
    public TMP_Text timerText;

    public bool isRunning;

    public void SetTimeByLevel(int level)
    {
        switch (level)
        {
            case 1: timeLeft = 120; break;
            case 2: timeLeft = 180; break;
            case 3: timeLeft = 240; break;
            case 4: timeLeft = 300; break;
        }

        isRunning = true;
    }

    void Update()
    {
        if (!isRunning) return;

        timeLeft -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        if (timeLeft <= 0)
        {
            Debug.Log("TIME OUT");
            isRunning = false;
        }
    }
}