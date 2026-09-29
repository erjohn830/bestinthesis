using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Settings Menu")]
    [SerializeField] private GameObject settingsMenu;

    [Header("Store Menu")]
    [SerializeField] private GameObject storeMenu;

    [Header("Volume Toggle")]
    [SerializeField] private Image volumeImage;
    [SerializeField] private Sprite volumeOnSprite;
    [SerializeField] private Sprite volumeOffSprite;

    private bool isVolumeOn = true;

    private void Start()
    {
        if (settingsMenu != null)
            settingsMenu.SetActive(false);

        if (storeMenu != null)
            storeMenu.SetActive(false);

        SetVolume(true);
    }

    // SETTINGS
    public void OpenSettings()
    {
        if (settingsMenu != null)
            settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsMenu != null)
            settingsMenu.SetActive(false);
    }

    // STORE
    public void OpenStore()
    {
        if (storeMenu != null)
            storeMenu.SetActive(true);
    }

    public void CloseStore()
    {
        if (storeMenu != null)
            storeMenu.SetActive(false);
    }

    // VOLUME
    public void ToggleVolume()
    {
        isVolumeOn = !isVolumeOn;
        SetVolume(isVolumeOn);
    }

    private void SetVolume(bool on)
    {
        if (volumeImage != null)
            volumeImage.sprite = on ? volumeOnSprite : volumeOffSprite;

        AudioListener.volume = on ? 1f : 0f;
    }

    
}