using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { Playing, GameOver, Victory }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState currentState = GameState.Playing;
    
    // UI panels
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public GameObject pausePanel;
    
    // Scene indices
    public int mainMenuSceneIndex = 0;
    public int gameplaySceneIndex = 1;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Ensure UI panels are initially hidden
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (victoryPanel) victoryPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
    }

    void Update()
    {
        if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void GameOver()
    {
        if (currentState != GameState.Playing) return;

        currentState = GameState.GameOver;
        Debug.Log("Game Over!");
        
        // Show Game Over UI
        if (gameOverPanel)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Victory()
    {
        if (currentState != GameState.Playing) return;

        currentState = GameState.Victory;
        Debug.Log("You Win!");
        
        // Show Victory UI
        if (victoryPanel)
        {
            victoryPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(gameplaySceneIndex);
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
    
    private void TogglePause()
    {
        if (currentState != GameState.Playing) return;
        
        if (pausePanel)
        {
            bool isPaused = pausePanel.activeSelf;
            pausePanel.SetActive(!isPaused);
            Time.timeScale = isPaused ? 1 : 0; // Pause/unpause game time
        }
    }
}
