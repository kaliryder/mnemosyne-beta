using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // Check if it's the first time running the game
        if (!PlayerPrefs.HasKey("FirstRun"))
        {
            // Initialize game data or perform first-run setup here

            // Mark that the first run has occurred
            PlayerPrefs.SetInt("FirstRun", 1);
            PlayerPrefs.Save();
        }
    }

    // Call this method to reset game data
    public void ResetGameData()
    {
        // Add code here to reset any necessary game data

        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}