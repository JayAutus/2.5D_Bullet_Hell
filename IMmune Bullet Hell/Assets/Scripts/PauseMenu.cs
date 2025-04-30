using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button mainMenuButton;

    void Start()
    {
        // Set up button listeners
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);
            
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            
        // Hide pause menu initially
        gameObject.SetActive(false);
    }
    
    void ResumeGame()
    {
        if (GameManager.instance != null)
            GameManager.instance.ResumeGame();
    }
    
    void GoToMainMenu()
    {
        if (GameManager.instance != null)
            GameManager.instance.LoadMainMenu();
    }
} 