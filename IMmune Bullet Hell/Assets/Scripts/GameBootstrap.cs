using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    void Awake()
    {
        // Create and initialize the game over screen
        GameObject gameOverBuilder = new GameObject("GameOverScreenBuilder");
        gameOverBuilder.AddComponent<GameOverScreenBuilder>();
        DontDestroyOnLoad(gameOverBuilder);
        
        Debug.Log("GameBootstrap: Initialized GameOverScreenBuilder");
    }
} 