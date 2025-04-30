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
    private GameObject canvasRoot; // Reference to the parent Canvas

    void Awake()
    {
        // Find the Canvas root (parent or grandparent that is a Canvas)
        Transform current = transform;
        while (current != null)
        {
            canvasRoot = current.gameObject;
            if (current.GetComponent<Canvas>() != null)
                break;
            current = current.parent;
        }

        if (canvasRoot == null)
        {
            Debug.LogError("GameOverScreen: Couldn't find parent Canvas!");
            canvasRoot = transform.gameObject; // Fallback to self
        }

        Debug.Log("GameOverScreen initialized. Canvas root: " + canvasRoot.name);
        
        // Start deactivated, deactivate the Canvas not this object
        if (canvasRoot != null && canvasRoot != gameObject)
            canvasRoot.SetActive(false);
        else
            gameObject.SetActive(false);
    }
    
    void Start()
    {
        // Set up button event listeners
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
        else
            Debug.LogError("GameOverScreen: Restart button not assigned!");
        
        if (mainMenuButton != null) 
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        else
            Debug.LogError("GameOverScreen: Main Menu button not assigned!");
        
        // Find the score manager
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
            Debug.LogWarning("GameOverScreen: ScoreManager not found!");
    }

    public void Show()
    {
        Debug.Log("GameOverScreen.Show() called");
        
        // Make sure we have the latest reference to ScoreManager
        if (scoreManager == null)
            scoreManager = FindObjectOfType<ScoreManager>();
            
        // Set final score
        if (scoreText != null && scoreManager != null)
        {
            int finalScore = scoreManager.GetCurrentScore();
            scoreText.text = "Final Score: " + finalScore;
            Debug.Log("Final score set: " + finalScore);
        }
        else
        {
            Debug.LogWarning("Cannot set score - missing references. scoreText: " + 
                (scoreText != null) + ", scoreManager: " + (scoreManager != null));
        }
        
        // Activate the game over screen - use the Canvas root
        if (canvasRoot != null)
        {
            canvasRoot.SetActive(true);
            Debug.Log("Activated Canvas: " + canvasRoot.name);
        }
        else
        {
            gameObject.SetActive(true);
            Debug.Log("Activated GameOverScreen object directly");
        }
    }

    void RestartGame()
    {
        Debug.Log("RestartGame called");
        // Restart current scene
        Time.timeScale = 1f; // Ensure normal time
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    
    void GoToMainMenu()
    {
        Debug.Log("GoToMainMenu called");
        // Load the main menu scene (assuming it's scene 0)
        Time.timeScale = 1f; // Ensure normal time
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
} 