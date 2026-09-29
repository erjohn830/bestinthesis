using UnityEngine;
using UnityEngine.UI;

public class PlayerHeart : MonoBehaviour
{
    public int lives = 3;
    public Transform spawnPoint;
    public GameObject player;
    public Image[] hearts;

    public void LoseHeart()
    {
        lives--;

        if (lives >= 0 && lives < hearts.Length)
            hearts[lives].gameObject.SetActive(false);

        if (lives <= 0)
            GameFlowManager.instance.GameOver();
        else
            Respawn();
    }

    void Respawn()
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        cc.enabled = false;
        player.transform.position = spawnPoint.position;
        cc.enabled = true;
    }
}