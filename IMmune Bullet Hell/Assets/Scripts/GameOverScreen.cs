using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public Button restartButton;
    public Button mainMenuButton;

    private ScoreManager scoreManager;

    void Awake()
    {
        // Start deactivated
        gameObject.SetActive(false);
    }
    
    void Start()
    {
        // Set up button event listeners
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
        
        if (mainMenuButton != null) 
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        
        // Find the score manager
        scoreManager = FindObjectOfType<ScoreManager>();
        
        // Make sure we're not active at start
        gameObject.SetActive(false);
    }

    public void Show()
    {
        // Make sure we have the latest reference to ScoreManager
        if (scoreManager == null)
            scoreManager = FindObjectOfType<ScoreManager>();
            
        // Set final score
        if (scoreText != null && scoreManager != null)
        {
            int finalScore = scoreManager.GetCurrentScore();
            scoreText.text = "Final Score: " + finalScore;
        }
        
        // Log that the screen is showing
        Debug.Log("Game Over Screen Activated");
        
        // Activate the game over screen
        gameObject.SetActive(true);
    }

    void RestartGame()
    {
        // Restart current scene
        Time.timeScale = 1f; // Ensure normal time
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    
    void GoToMainMenu()
    {
        // Load the main menu scene (assuming it's scene 0)
        Time.timeScale = 1f; // Ensure normal time
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
} 