using UnityEngine;

public class GameManagerExtension : MonoBehaviour
{
    private static GameObject builderInstance;
    
    void Awake()
    {
        // Create the GameOverScreenBuilder if it doesn't exist yet
        if (builderInstance == null)
        {
            builderInstance = new GameObject("GameOverScreenBuilder");
            builderInstance.AddComponent<GameOverScreenBuilder>();
            DontDestroyOnLoad(builderInstance);
            Debug.Log("GameOverScreenBuilder created by GameManagerExtension");
        }
    }
    
    // Method to be called from GameManager to show game over screen
    public static void ShowGameOver()
    {
        if (GameOverScreenBuilder.Instance != null)
        {
            GameOverScreenBuilder.Instance.ShowGameOver();
            Debug.Log("GameOver shown via GameManagerExtension");
        }
        else
        {
            Debug.LogError("GameOverScreenBuilder.Instance is null!");
            
            // Try to create it as a last resort
            if (builderInstance == null)
            {
                builderInstance = new GameObject("GameOverScreenBuilder");
                builderInstance.AddComponent<GameOverScreenBuilder>();
                Debug.Log("GameOverScreenBuilder created as last resort");
                
                // Give it a frame to initialize
                new GameObject("DelayedGameOverShower").AddComponent<DelayedGameOverShower>();
            }
        }
    }
}

// Helper class to show game over screen after a delay
public class DelayedGameOverShower : MonoBehaviour
{
    void Start()
    {
        Invoke("ShowDelayed", 0.1f);
    }
    
    void ShowDelayed()
    {
        if (GameOverScreenBuilder.Instance != null)
        {
            GameOverScreenBuilder.Instance.ShowGameOver();
            Debug.Log("GameOver shown after delay");
        }
        Destroy(gameObject);
    }
} 