using UnityEngine;

public class TimingBar : MonoBehaviour
{
    [Header("UI")]
    public RectTransform marker;
    public RectTransform yellowZone;

    [Header("Speed Settings")]
    public float baseSpeed = 150f;  
    public float speedIncrease = 20f; 
    private float speed;

    public JumpJudge judge;

    float direction = 1;
    bool active = false;

    void Start()
    {
        SetLevel(1); 
    }

    void Update()
    {
        if (!active) return;

        marker.anchoredPosition += Vector2.right * speed * direction * Time.unscaledDeltaTime;

        if (marker.anchoredPosition.x > 300 || marker.anchoredPosition.x < -300)
            direction *= -1;
    }

    public void SetLevel(int level)
    {
        speed = baseSpeed + (level * speedIncrease);
    }

    public void Activate()
    {
        active = true;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        active = false;
        gameObject.SetActive(false);
    }

    public void CheckTiming()
    {
        active = false;

        float markerX = marker.anchoredPosition.x;
        float zoneMin = yellowZone.anchoredPosition.x - (yellowZone.rect.width / 2);
        float zoneMax = yellowZone.anchoredPosition.x + (yellowZone.rect.width / 2);

        if (markerX >= zoneMin && markerX <= zoneMax)
        {
            judge.PerfectJump();
        }
        else
        {
            judge.BadJump();
        }

        Deactivate();
    }
}