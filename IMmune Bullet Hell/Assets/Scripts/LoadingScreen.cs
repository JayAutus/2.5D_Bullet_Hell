using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;
    public float fadeSpeed = 2f;
    public CanvasGroup canvasGroup;
    
    private void Awake()
    {
        // If Loading Screen activated, start loading next scene
        if (gameObject.activeInHierarchy && canvasGroup != null)
        {
            // Make sure it's visible
            canvasGroup.alpha = 1;
            
            // Start async load process
            StartCoroutine(LoadNextScene());
        }
    }
    
    private IEnumerator LoadNextScene()
    {
        // Get the next scene to load
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentIndex + 1;
        
        // Start async load
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex);
        asyncLoad.allowSceneActivation = false;
        
        // Update progress bar
        while (!asyncLoad.isDone)
        {
            if (progressBar != null)
                progressBar.value = asyncLoad.progress;
                
            // When load is nearly complete
            if (asyncLoad.progress >= 0.9f)
            {
                // Allow activation after a brief pause
                yield return new WaitForSeconds(1f);
                asyncLoad.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
    
    // Can be called manually to load a specific scene
    public void LoadScene(int sceneIndex)
    {
        gameObject.SetActive(true);
        
        if (canvasGroup != null)
            canvasGroup.alpha = 1;
            
        StartCoroutine(LoadSpecificScene(sceneIndex));
    }
    
    private IEnumerator LoadSpecificScene(int index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        asyncLoad.allowSceneActivation = false;
        
        while (!asyncLoad.isDone)
        {
            if (progressBar != null)
                progressBar.value = asyncLoad.progress;
                
            if (asyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                asyncLoad.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
} 