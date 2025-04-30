using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingImage;
    public GameObject settingsPanel;
    public Button playButton;
    public Button quitButton;
    public Button settingsButton;
    public Button backButton;

    void Start()
    {
        // Hide loading image initially
        if (loadingImage != null)
            loadingImage.SetActive(false);
            
        // Hide settings panel initially
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
            
        // Setup button listeners
        if (playButton != null)
            playButton.onClick.AddListener(() => LoadScene(1)); // Game scene
            
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
            
        if (settingsButton != null)
            settingsButton.onClick.AddListener(ToggleSettings);
            
        if (backButton != null)
            backButton.onClick.AddListener(ToggleSettings);
    }

    public void LoadScene(int level)
    {
        // Show loading image
        if (loadingImage != null)
            loadingImage.SetActive(true);

        // Load the scene
        SceneManager.LoadScene(level);
    }
    
    void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    void ToggleSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
