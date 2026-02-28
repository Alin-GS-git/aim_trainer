using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Called when Static Mode button is clicked
    public void LoadStaticMode()
    {
        SceneManager.LoadScene("StaticMode"); // Make sure scene name matches exactly
    }

    // Called when Moving Mode button is clicked
    public void LoadMovingMode()
    {
        SceneManager.LoadScene("MovingMode"); // Make sure scene name matches exactly
    }

    // Optional: Quit button
    public void QuitGame()
    {
        Application.Quit();
    }
}