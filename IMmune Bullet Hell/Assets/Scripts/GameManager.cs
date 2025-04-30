using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Playing, GameOver, Victory }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerHealthTest playerHealth;

    public GameState currentState = GameState.Playing;
    
    void Start()
    {
        // Find the player in the scene (one way)
        playerHealth = FindObjectOfType<PlayerHealthTest>();
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (playerHealth.currentHealth <= 0){
            currentState=GameState.GameOver;
        }
        
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
        // Optional: Show Game Over UI
    }

    public void Victory()
    {
        if (currentState != GameState.Playing) return;

        currentState = GameState.Victory;
        Debug.Log("You Win!");
        // Optional: Show Victory UI
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
