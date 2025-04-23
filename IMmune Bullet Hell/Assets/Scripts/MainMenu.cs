using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingImage;
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    public GameObject instructionsPanel;
    
    // Scene indices
    public int gameplaySceneIndex = 1;
    
    void Start()
    {
        // Ensure panels are set up correctly
        if (loadingImage) loadingImage.SetActive(false);
        if (creditsPanel) creditsPanel.SetActive(false);
        if (instructionsPanel) instructionsPanel.SetActive(false);
        if (mainMenuPanel) mainMenuPanel.SetActive(true);
    }

    public void StartGame()
    {
        if (loadingImage) loadingImage.SetActive(true);
        SceneManager.LoadScene(gameplaySceneIndex);
    }
    
    public void LoadScene(int sceneIndex)
    {
        if (loadingImage) loadingImage.SetActive(true);
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    
    public void ShowInstructions()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }
    
    public void ReturnToMainMenu()
    {
        if (creditsPanel) creditsPanel.SetActive(false);
        if (instructionsPanel) instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 