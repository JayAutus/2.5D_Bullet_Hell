using UnityEngine;

public class DebugGameOver : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerHealthTest playerHealth;
    private GameOverScreen gameOverScreen;
    
    // This can be attached to any GameObject in the scene for testing
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerHealth = FindObjectOfType<PlayerHealthTest>();
        gameOverScreen = FindObjectOfType<GameOverScreen>();
        
        Debug.Log("DebugGameOver initialized");
        LogStatus();
    }
    
    void LogStatus()
    {
        Debug.Log("GameManager exists: " + (gameManager != null));
        Debug.Log("PlayerHealthTest exists: " + (playerHealth != null));
        Debug.Log("GameOverScreen exists: " + (gameOverScreen != null));
        
        if (gameOverScreen != null)
        {
            Debug.Log("GameOverScreen active: " + gameOverScreen.gameObject.activeInHierarchy);
            Debug.Log("GameOverScreen components check:");
            Debug.Log(" - gameOverText: " + (gameOverScreen.gameOverText != null));
            Debug.Log(" - scoreText: " + (gameOverScreen.scoreText != null));
            Debug.Log(" - restartButton: " + (gameOverScreen.restartButton != null));
            Debug.Log(" - mainMenuButton: " + (gameOverScreen.mainMenuButton != null));
        }
    }
    
    // Can be called from a UI button for testing
    public void TestGameOver()
    {
        Debug.Log("Testing Game Over...");
        
        if (gameManager != null)
        {
            gameManager.GameOver();
            Debug.Log("GameOver called on GameManager");
        }
        else if (gameOverScreen != null)
        {
            gameOverScreen.Show();
            Debug.Log("Show called directly on GameOverScreen");
        }
        else
        {
            Debug.LogError("Could not test Game Over - missing components");
        }
        
        LogStatus();
    }
    
    // Can be called from a UI button to force player death
    public void KillPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.currentHealth = 0;
            playerHealth.TakeDamage(1); // This should trigger Die()
            Debug.Log("Forced player death");
        }
        else
        {
            Debug.LogError("Could not kill player - PlayerHealthTest not found");
        }
    }
} 