using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Pause the game by setting the time scale to 0
    public void Pause(){
        Time.timeScale = 0;
    }

    // Resume the game by setting the time scale back to 1
    public void Play(){
        Time.timeScale = 1;
    }

    // Quit the application/game
    public void QuitGame()
    {
        Application.Quit();
    }
}
