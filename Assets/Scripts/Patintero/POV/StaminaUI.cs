using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUI : MonoBehaviour
{
    public UserMovement player;
    public Image staminaBar;

    void Update()
    {
        if (player == null) return;

        float percent = player.stamina / player.maxStamina;
        staminaBar.fillAmount = percent;

        staminaBar.color = (player.stamina <= 0) ? Color.red : Color.green;
    }
}