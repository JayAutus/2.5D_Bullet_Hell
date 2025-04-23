using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Text scoreText;
    public Button restartButton;
    public Button mainMenuButton;
    
    // Scene indices
    public int mainMenuSceneIndex = 0;
    
    void Start()
    {
        // Set up button click events
        if (restartButton)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
        
        if (mainMenuButton)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
        
        // Set final score if available
        if (scoreText && ScoreManager.instance)
        {
            scoreText.text = "Final Score: " + ScoreManager.instance.GetScore().ToString();
        }
    }
    
    public void RestartGame()
    {
        // Use GameManager if available, otherwise load current scene again
        if (GameManager.instance)
        {
            GameManager.instance.RestartGame();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    public void ReturnToMainMenu()
    {
        // Use GameManager if available, otherwise load main menu scene
        if (GameManager.instance)
        {
            GameManager.instance.ReturnToMainMenu();
        }
        else
        {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }
    }
} 