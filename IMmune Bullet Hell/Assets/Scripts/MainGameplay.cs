using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainGameplay : MonoBehaviour
{
    public GameOverScreen gameOverScreenPrefab;
    public GameObject gameManagerPrefab;
    
    void Awake()
    {
        // Make sure we have a GameManager in the scene
        if (GameManager.instance == null && gameManagerPrefab != null)
        {
            Instantiate(gameManagerPrefab);
            Debug.Log("GameManager instantiated by MainGameplay");
        }
        
        // Make sure we have a GameOverScreen in the scene - first check if it exists
        if (FindObjectOfType<GameOverScreen>() == null)
        {
            if (gameOverScreenPrefab != null)
            {
                Instantiate(gameOverScreenPrefab);
                Debug.Log("GameOverScreen prefab instantiated by MainGameplay");
            }
            else
            {
                // If no prefab is assigned, create one programmatically
                CreateGameOverScreen();
                Debug.Log("Created GameOverScreen programmatically");
            }
        }
    }
    
    void CreateGameOverScreen()
    {
        // Create Canvas
        GameObject canvas = new GameObject("GameOverCanvas");
        Canvas canvasComponent = canvas.AddComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasComponent.sortingOrder = 10; // Ensure it's on top
        
        // Add CanvasScaler
        CanvasScaler scaler = canvas.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        // Add GraphicRaycaster
        canvas.AddComponent<GraphicRaycaster>();
        
        // Create Panel
        GameObject panel = new GameObject("GameOverPanel");
        panel.transform.SetParent(canvas.transform, false);
        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.8f);
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;
        
        // Add GameOverScreen component
        GameOverScreen gameOverScreen = panel.AddComponent<GameOverScreen>();
        
        // Create Game Over Text
        GameObject gameOverTextObj = new GameObject("GameOverText");
        gameOverTextObj.transform.SetParent(panel.transform, false);
        TextMeshProUGUI gameOverText = gameOverTextObj.AddComponent<TextMeshProUGUI>();
        gameOverText.text = "GAME OVER";
        gameOverText.fontSize = 72;
        gameOverText.alignment = TextAlignmentOptions.Center;
        gameOverText.color = Color.red;
        RectTransform gameOverTextRect = gameOverTextObj.GetComponent<RectTransform>();
        gameOverTextRect.anchorMin = new Vector2(0.5f, 0.7f);
        gameOverTextRect.anchorMax = new Vector2(0.5f, 0.9f);
        gameOverTextRect.sizeDelta = new Vector2(500, 100);
        gameOverTextRect.anchoredPosition = Vector2.zero;
        
        // Create Score Text
        GameObject scoreTextObj = new GameObject("ScoreText");
        scoreTextObj.transform.SetParent(panel.transform, false);
        TextMeshProUGUI scoreText = scoreTextObj.AddComponent<TextMeshProUGUI>();
        scoreText.text = "Final Score: 0";
        scoreText.fontSize = 48;
        scoreText.alignment = TextAlignmentOptions.Center;
        scoreText.color = Color.white;
        RectTransform scoreTextRect = scoreTextObj.GetComponent<RectTransform>();
        scoreTextRect.anchorMin = new Vector2(0.5f, 0.5f);
        scoreTextRect.anchorMax = new Vector2(0.5f, 0.6f);
        scoreTextRect.sizeDelta = new Vector2(400, 80);
        scoreTextRect.anchoredPosition = Vector2.zero;
        
        // Create Restart Button
        GameObject restartButtonObj = new GameObject("RestartButton");
        restartButtonObj.transform.SetParent(panel.transform, false);
        Image restartButtonImage = restartButtonObj.AddComponent<Image>();
        restartButtonImage.color = new Color(0.2f, 0.2f, 0.2f, 1);
        Button restartButton = restartButtonObj.AddComponent<Button>();
        restartButton.targetGraphic = restartButtonImage;
        RectTransform restartButtonRect = restartButtonObj.GetComponent<RectTransform>();
        restartButtonRect.anchorMin = new Vector2(0.5f, 0.3f);
        restartButtonRect.anchorMax = new Vector2(0.5f, 0.4f);
        restartButtonRect.sizeDelta = new Vector2(300, 60);
        restartButtonRect.anchoredPosition = Vector2.zero;
        
        // Restart Button Text
        GameObject restartTextObj = new GameObject("RestartText");
        restartTextObj.transform.SetParent(restartButtonObj.transform, false);
        TextMeshProUGUI restartText = restartTextObj.AddComponent<TextMeshProUGUI>();
        restartText.text = "Restart";
        restartText.fontSize = 36;
        restartText.alignment = TextAlignmentOptions.Center;
        restartText.color = Color.white;
        RectTransform restartTextRect = restartTextObj.GetComponent<RectTransform>();
        restartTextRect.anchorMin = Vector2.zero;
        restartTextRect.anchorMax = Vector2.one;
        restartTextRect.sizeDelta = Vector2.zero;
        
        // Create Main Menu Button
        GameObject menuButtonObj = new GameObject("MainMenuButton");
        menuButtonObj.transform.SetParent(panel.transform, false);
        Image menuButtonImage = menuButtonObj.AddComponent<Image>();
        menuButtonImage.color = new Color(0.2f, 0.2f, 0.2f, 1);
        Button menuButton = menuButtonObj.AddComponent<Button>();
        menuButton.targetGraphic = menuButtonImage;
        RectTransform menuButtonRect = menuButtonObj.GetComponent<RectTransform>();
        menuButtonRect.anchorMin = new Vector2(0.5f, 0.15f);
        menuButtonRect.anchorMax = new Vector2(0.5f, 0.25f);
        menuButtonRect.sizeDelta = new Vector2(300, 60);
        menuButtonRect.anchoredPosition = Vector2.zero;
        
        // Menu Button Text
        GameObject menuTextObj = new GameObject("MenuText");
        menuTextObj.transform.SetParent(menuButtonObj.transform, false);
        TextMeshProUGUI menuText = menuTextObj.AddComponent<TextMeshProUGUI>();
        menuText.text = "Main Menu";
        menuText.fontSize = 36;
        menuText.alignment = TextAlignmentOptions.Center;
        menuText.color = Color.white;
        RectTransform menuTextRect = menuTextObj.GetComponent<RectTransform>();
        menuTextRect.anchorMin = Vector2.zero;
        menuTextRect.anchorMax = Vector2.one;
        menuTextRect.sizeDelta = Vector2.zero;
        
        // Assign references to GameOverScreen component
        gameOverScreen.gameOverText = gameOverText;
        gameOverScreen.scoreText = scoreText;
        gameOverScreen.restartButton = restartButton;
        gameOverScreen.mainMenuButton = menuButton;
        
        // Hide at start
        panel.SetActive(false);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("MainGameplay initialized");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
