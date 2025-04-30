using UnityEngine;

// This script can be added to any GameObject in the scene to test the GameOverScreen
public class DebugGameOverTester : MonoBehaviour
{
    private GameOverScreen gameOverScreen;
    private Canvas gameOverCanvas;
    
    void Start()
    {
        Debug.Log("DebugGameOverTester started. Press F10 to test showing GameOverScreen directly.");
        
        // Find the GameOverScreen
        gameOverScreen = FindObjectOfType<GameOverScreen>();
        if (gameOverScreen == null)
        {
            Debug.LogError("No GameOverScreen found in scene!");
            return;
        }
        
        Debug.Log("Found GameOverScreen: " + gameOverScreen.gameObject.name);
        
        // Find the Canvas component
        Transform current = gameOverScreen.transform;
        while (current != null)
        {
            gameOverCanvas = current.GetComponent<Canvas>();
            if (gameOverCanvas != null)
                break;
            current = current.parent;
        }
        
        if (gameOverCanvas != null)
            Debug.Log("Found Canvas: " + gameOverCanvas.gameObject.name);
        else
            Debug.LogError("No Canvas found as a parent of GameOverScreen!");
    }
    
    void Update()
    {
        // Press F10 to show the game over screen directly
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Debug.Log("F10 pressed - showing GameOverScreen directly");
            ShowGameOverScreenDirectly();
        }
        
        // Press F11 to activate the Canvas directly
        if (Input.GetKeyDown(KeyCode.F11) && gameOverCanvas != null)
        {
            Debug.Log("F11 pressed - activating Canvas directly");
            gameOverCanvas.gameObject.SetActive(true);
        }
    }
    
    void ShowGameOverScreenDirectly()
    {
        if (gameOverScreen == null)
        {
            Debug.LogError("Cannot show GameOverScreen - not found!");
            return;
        }
        
        try
        {
            gameOverScreen.Show();
            Debug.Log("GameOverScreen.Show() called directly from DebugGameOverTester");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error showing GameOverScreen: " + e.Message);
        }
    }
} 