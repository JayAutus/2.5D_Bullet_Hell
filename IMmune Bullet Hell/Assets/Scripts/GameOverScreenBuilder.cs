using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverScreenBuilder : MonoBehaviour
{
    // Allows this component to persist and be accessible throughout the application
    public static GameOverScreenBuilder Instance { get; private set; }
    
    private GameObject canvasObject;
    private GameObject panelObject;
    private GameOverScreen gameOverScreenComponent;
    
    private bool isInitialized = false;
    
    void Awake()
    {
        // Singleton pattern with robust handling for scene changes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Make sure we build the screen
            BuildGameOverScreen();
            Debug.Log("GameOverScreenBuilder initialized (first instance)");
        }
        else if (Instance != this)
        {
            Debug.Log("Destroying duplicate GameOverScreenBuilder");
            Destroy(gameObject);
        }
    }
    
    void OnEnable()
    {
        // Register for scene change events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unregister to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name + ". Hiding game over screen.");
        // Hide the game over UI when new scenes are loaded
        HideGameOver();
    }
    
    public void BuildGameOverScreen()
    {
        Debug.Log("Building GameOverScreen...");
        
        if (isInitialized)
        {
            Debug.Log("GameOverScreen already built!");
            return;
        }
        
        // Create Canvas
        canvasObject = new GameObject("_GameOverCanvas");
        DontDestroyOnLoad(canvasObject);
        canvasObject.transform.SetParent(transform);
        
        Canvas canvasComponent = canvasObject.AddComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasComponent.sortingOrder = 100; // Very high to be above everything
        
        // Add CanvasScaler
        CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        // Add GraphicRaycaster
        canvasObject.AddComponent<GraphicRaycaster>();
        
        // Find the Electronic Highway Sign font
        TMP_FontAsset fontAsset = Resources.Load<TMP_FontAsset>("Fonts & Materials/Electronic Highway Sign SDF");
        if (fontAsset == null)
        {
            // Try alternative paths
            fontAsset = Resources.Load<TMP_FontAsset>("Fonts/Electronic Highway Sign SDF");
        }
        
        if (fontAsset == null)
        {
            Debug.LogWarning("Could not find Electronic Highway Sign font, using default font instead");
        }
        
        // Create Panel with blur effect background
        panelObject = new GameObject("GameOverPanel");
        panelObject.transform.SetParent(canvasObject.transform, false);
        
        Image panelImage = panelObject.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.85f); // More opaque for better contrast
        
        RectTransform panelRect = panelObject.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;
        panelRect.anchoredPosition = Vector2.zero;
        
        // Add GameOverScreen component
        gameOverScreenComponent = panelObject.AddComponent<GameOverScreen>();
        
        // Create Game Over Text with glow effect
        GameObject titleObject = new GameObject("GameOverText");
        titleObject.transform.SetParent(panelObject.transform, false);
        
        TextMeshProUGUI titleText = titleObject.AddComponent<TextMeshProUGUI>();
        titleText.text = "GAME OVER";
        titleText.fontSize = 180; // Super big text
        titleText.color = Color.red;
        titleText.alignment = TextAlignmentOptions.Center;
        
        // Add gradient for visual interest
        titleText.enableVertexGradient = true;
        titleText.colorGradient = new VertexGradient(
            new Color(1f, 0.1f, 0.1f), // top left - bright red
            new Color(1f, 0.3f, 0.1f), // top right - orange-red
            new Color(0.8f, 0.1f, 0.1f), // bottom left - darker red
            new Color(0.9f, 0.2f, 0.1f)  // bottom right - medium red
        );
        
        // Set font if we found it
        if (fontAsset != null)
            titleText.font = fontAsset;
        
        RectTransform titleRect = titleObject.GetComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0.5f, 0.65f);
        titleRect.anchorMax = new Vector2(0.5f, 0.95f);
        titleRect.sizeDelta = new Vector2(1000, 200);
        titleRect.anchoredPosition = Vector2.zero;
        
        // Create Score Text
        GameObject scoreObject = new GameObject("ScoreText");
        scoreObject.transform.SetParent(panelObject.transform, false);
        
        TextMeshProUGUI scoreText = scoreObject.AddComponent<TextMeshProUGUI>();
        scoreText.text = "FINAL SCORE: 0";
        scoreText.fontSize = 60;
        scoreText.color = Color.white;
        scoreText.alignment = TextAlignmentOptions.Center;
        
        // Set font if we found it
        if (fontAsset != null)
            scoreText.font = fontAsset;
        
        RectTransform scoreRect = scoreObject.GetComponent<RectTransform>();
        scoreRect.anchorMin = new Vector2(0.5f, 0.5f);
        scoreRect.anchorMax = new Vector2(0.5f, 0.6f);
        scoreRect.sizeDelta = new Vector2(600, 100);
        scoreRect.anchoredPosition = Vector2.zero;
        
        // Create Restart Button with custom styling - White with black text
        GameObject restartBtnObject = new GameObject("RestartButton");
        restartBtnObject.transform.SetParent(panelObject.transform, false);
        
        Image restartBtnImage = restartBtnObject.AddComponent<Image>();
        restartBtnImage.color = Color.white; // White background
        
        Button restartBtn = restartBtnObject.AddComponent<Button>();
        restartBtn.targetGraphic = restartBtnImage;
        
        // Change button colors for better feedback
        ColorBlock restartColors = restartBtn.colors;
        restartColors.normalColor = Color.white;
        restartColors.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1f); // Slightly darker white when highlighted
        restartColors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Even darker when pressed
        restartColors.selectedColor = Color.white;
        restartBtn.colors = restartColors;
        
        RectTransform restartBtnRect = restartBtnObject.GetComponent<RectTransform>();
        restartBtnRect.anchorMin = new Vector2(0.5f, 0.32f);
        restartBtnRect.anchorMax = new Vector2(0.5f, 0.38f);
        restartBtnRect.sizeDelta = new Vector2(600, 60); // Wider but less height
        
        // Add Restart Button Text
        GameObject restartTextObject = new GameObject("RestartText");
        restartTextObject.transform.SetParent(restartBtnObject.transform, false);
        
        TextMeshProUGUI restartText = restartTextObject.AddComponent<TextMeshProUGUI>();
        restartText.text = "RESTART";
        restartText.fontSize = 42;
        restartText.color = Color.black; // Black text on white button
        restartText.alignment = TextAlignmentOptions.Center;
        
        // Set font if we found it
        if (fontAsset != null)
            restartText.font = fontAsset;
        
        RectTransform restartTextRect = restartTextObject.GetComponent<RectTransform>();
        restartTextRect.anchorMin = Vector2.zero;
        restartTextRect.anchorMax = Vector2.one;
        restartTextRect.sizeDelta = Vector2.zero;
        restartTextRect.anchoredPosition = Vector2.zero;
        
        // Create Main Menu Button - Red with white text
        GameObject menuBtnObject = new GameObject("MainMenuButton");
        menuBtnObject.transform.SetParent(panelObject.transform, false);
        
        Image menuBtnImage = menuBtnObject.AddComponent<Image>();
        menuBtnImage.color = new Color(0.9f, 0.1f, 0.1f, 1f); // Red background
        
        Button menuBtn = menuBtnObject.AddComponent<Button>();
        menuBtn.targetGraphic = menuBtnImage;
        
        // Red button colors
        ColorBlock menuColors = menuBtn.colors;
        menuColors.normalColor = new Color(0.9f, 0.1f, 0.1f, 1f); // Red
        menuColors.highlightedColor = new Color(1f, 0.2f, 0.2f, 1f); // Brighter red when highlighted
        menuColors.pressedColor = new Color(0.7f, 0.1f, 0.1f, 1f); // Darker red when pressed
        menuColors.selectedColor = new Color(0.9f, 0.1f, 0.1f, 1f);
        menuBtn.colors = menuColors;
        
        RectTransform menuBtnRect = menuBtnObject.GetComponent<RectTransform>();
        menuBtnRect.anchorMin = new Vector2(0.5f, 0.22f);
        menuBtnRect.anchorMax = new Vector2(0.5f, 0.28f);
        menuBtnRect.sizeDelta = new Vector2(600, 60); // Same size as restart button
        
        // Add Main Menu Button Text
        GameObject menuTextObject = new GameObject("MenuText");
        menuTextObject.transform.SetParent(menuBtnObject.transform, false);
        
        TextMeshProUGUI menuText = menuTextObject.AddComponent<TextMeshProUGUI>();
        menuText.text = "MAIN MENU";
        menuText.fontSize = 42;
        menuText.color = Color.white; // White text on red button
        menuText.alignment = TextAlignmentOptions.Center;
        
        // Set font if we found it
        if (fontAsset != null)
            menuText.font = fontAsset;
        
        RectTransform menuTextRect = menuTextObject.GetComponent<RectTransform>();
        menuTextRect.anchorMin = Vector2.zero;
        menuTextRect.anchorMax = Vector2.one;
        menuTextRect.sizeDelta = Vector2.zero;
        menuTextRect.anchoredPosition = Vector2.zero;
        
        // Assign references to GameOverScreen component
        gameOverScreenComponent.gameOverText = titleText;
        gameOverScreenComponent.scoreText = scoreText;
        gameOverScreenComponent.restartButton = restartBtn;
        gameOverScreenComponent.mainMenuButton = menuBtn;
        
        // Set up button actions
        restartBtn.onClick.AddListener(RestartGame);
        menuBtn.onClick.AddListener(GoToMainMenu);
        
        // Hide at start
        canvasObject.SetActive(false);
        
        isInitialized = true;
        Debug.Log("GameOverScreen built successfully!");
    }
    
    public GameOverScreen GetGameOverScreen()
    {
        if (!isInitialized)
            BuildGameOverScreen();
        
        return gameOverScreenComponent;
    }
    
    public void ShowGameOver()
    {
        if (!isInitialized)
            BuildGameOverScreen();
        
        Debug.Log("Showing GameOverScreen from builder...");
        
        // Update score if ScoreManager exists
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null && gameOverScreenComponent.scoreText != null)
        {
            gameOverScreenComponent.scoreText.text = "Final Score: " + scoreManager.GetCurrentScore();
        }
        
        // Show the canvas
        canvasObject.SetActive(true);
    }
    
    public void HideGameOver()
    {
        Debug.Log("HideGameOver called");
        
        if (!isInitialized)
        {
            Debug.Log("GameOverScreen not initialized, nothing to hide");
            return;
        }
        
        if (canvasObject != null)
        {
            canvasObject.SetActive(false);
            Debug.Log("GameOverScreen canvas hidden");
        }
        else
        {
            Debug.LogWarning("Canvas object is null, cannot hide");
        }
    }
    
    private void RestartGame()
    {
        Debug.Log("Restarting game...");
        // Hide the game over UI
        HideGameOver();
        
        // Reset time scale before loading new scene
        Time.timeScale = 1f;
        
        // Add a slight delay before loading the scene to ensure UI is hidden
        StartCoroutine(LoadSceneWithDelay(SceneManager.GetActiveScene().buildIndex, 0.1f));
    }
    
    private void GoToMainMenu()
    {
        Debug.Log("Going to main menu...");
        // Hide the game over UI
        HideGameOver();
        
        // Reset time scale before loading new scene
        Time.timeScale = 1f;
        
        // Add a slight delay before loading the scene to ensure UI is hidden
        StartCoroutine(LoadSceneWithDelay(0, 0.1f)); // Assuming main menu is scene 0
    }
    
    private IEnumerator LoadSceneWithDelay(int sceneIndex, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneIndex);
    }
} 