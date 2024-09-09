using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load the scene named "Main Level" when the Play button is clicked
    public void PlayGame()
    {
        SceneManager.LoadScene("Main Level");
    }

    // Quit the application/game when the Quit button is clicked
    public void QuitGame()
    {
        Application.Quit();
    }
}
