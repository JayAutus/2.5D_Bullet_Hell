using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Playing, GameOver, Victory, Paused }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerHealthTest playerHealth;
    private GameOverScreen gameOverScreen;
    
    public GameState currentState = GameState.Playing;
    public GameObject pauseMenuUI;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }
    
    void Start()
    {
        // Find the player in the scene
        playerHealth = FindObjectOfType<PlayerHealthTest>();
        
        // Find the game over screen
        gameOverScreen = FindObjectOfType<GameOverScreen>();
        
        // Make sure pause menu is hidden at start
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
            
        // Make sure game over screen is hidden at start
        if (gameOverScreen != null)
            gameOverScreen.gameObject.SetActive(false);
            
        Time.timeScale = 1f; // Ensure normal time
    }

    void Update()
    {
        // Check if player is dead
        if (playerHealth != null && playerHealth.currentHealth <= 0 && currentState == GameState.Playing)
        {
            GameOver();
        }
        
        // Handle pause menu toggle
        if (Input.GetKeyDown(KeyCode.Escape) && currentState != GameState.GameOver)
        {
            if (currentState == GameState.Paused)
                ResumeGame();
            else
                PauseGame();
        }
        
        // Restart with R key during game over
        if (currentState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        if (currentState != GameState.Playing) return;

        currentState = GameState.GameOver;
        Debug.Log("Game Over!");
        
        // Show game over screen
        if (gameOverScreen != null)
            gameOverScreen.Show();
            
        // Optional: Slow down time
        Time.timeScale = 0.5f;
    }

    public void Victory()
    {
        if (currentState != GameState.Playing) return;

        currentState = GameState.Victory;
        Debug.Log("You Win!");
        // Show victory UI
    }
    
    public void PauseGame()
    {
        if (currentState == GameState.Playing)
        {
            currentState = GameState.Paused;
            Time.timeScale = 0f;
            if (pauseMenuUI != null)
                pauseMenuUI.SetActive(true);
        }
    }
    
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            currentState = GameState.Playing;
            Time.timeScale = 1f;
            if (pauseMenuUI != null)
                pauseMenuUI.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Assuming main menu is scene 0
    }
}
