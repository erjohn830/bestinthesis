using UnityEngine;
using TMPro;

public class GameTiming : MonoBehaviour
{
    public float timeRemaining = 480f;
    public TextMeshProUGUI timerText;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            timerText.text = minutes + ":" + seconds.ToString("00");
        }
        else
        {
            GameFlowManager.instance.GameOver();
        }
    }
}