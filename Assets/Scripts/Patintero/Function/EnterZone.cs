using UnityEngine;

public class EnterZone : MonoBehaviour
{
    public GameObject tayaManager;
    public ThirdCamView cam;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Player starts crossing
        if (GameFlowManager.instance.playerState == GameFlowManager.PlayerState.Lobby)
        {
            GameFlowManager.instance.playerState = GameFlowManager.PlayerState.Crossing;

            if (tayaManager != null)
                tayaManager.SetActive(true);
        }
        // Player returns from EndZone
        else if (GameFlowManager.instance.playerState == GameFlowManager.PlayerState.Returning)
        
            // Reset camera back to default view
            if (cam != null)
                cam.ResetToDefaultView();

            GameFlowManager.instance.WinLevel();
    }
}