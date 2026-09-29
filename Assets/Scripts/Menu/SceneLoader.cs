using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStory()
    {
        SceneManager.LoadScene("LuksongBakaStory");
    }

    public void LoaGamecarousel()
    {
        SceneManager.LoadScene("Gamecarousel"); // loading other scene
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("LuksongBakaGame");
    }

    public void LoadMultiplayer()
    {
        SceneManager.LoadScene("multiplayer");
    }

    public void Loadstory2()
    {
        SceneManager.LoadScene("PatinteroStory");
    }

    public void LoadPantintero()
    {
        SceneManager.LoadScene("PatinteroGame");
    }

    public void LoadLuksongbakaHistory()
    {
        SceneManager.LoadScene("history");
    }

    public void LoadPantinterHistory()
    {
        SceneManager.LoadScene("Patinterohistory");
    }

    public void LoadiceicewaterHistory()
    {
        SceneManager.LoadScene("iceicewaterhistory");
    }

    public void LoadiceicewaterGame()
    {
        SceneManager.LoadScene("IceWaterGame");
    }

    public void LoadStory3()
    {
        SceneManager.LoadScene("iceicewaterstory");
    }

     public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

   

}