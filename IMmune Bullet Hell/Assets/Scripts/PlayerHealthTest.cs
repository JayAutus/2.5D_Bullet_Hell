using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthTest : MonoBehaviour {

    public int startingHealth = 3;
    public int currentHealth;
    public Slider healthSlider;

    public GameObject ExplosionPrefab;

    public GameObject Hitsparks;
    
    private bool isDead = false;

    // This reference shouldn't be needed since GameManager handles the GameOverScreen
    // public GameObject gameOverPanel;
    private GameOverScreen gameOverScreen;

    void Awake ()
    {
        currentHealth = startingHealth;
        gameOverScreen = FindObjectOfType<GameOverScreen>();
    }

    public void TakeDamage (int amount)
    {
        // Don't take damage if already dead
        if (isDead) return;
        
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        Instantiate(Hitsparks, transform.position, Quaternion.identity);
        
        if(currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }
    
    void Die()
    {
        // Set flag to prevent multiple calls
        isDead = true;
        
        Debug.Log("Player died!");
        
        if (ExplosionPrefab != null)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        }

        // Notify game manager
        if (GameManager.instance != null)
        {
            GameManager.instance.GameOver();
            Debug.Log("Notified GameManager of death");
        }
        else
        {
            Debug.LogWarning("GameManager.instance is null! Make sure GameManager exists in the scene.");
            
            // Fallback if GameManager isn't available
            if (gameOverScreen != null)
            {
                gameOverScreen.Show();
                Debug.Log("Showing game over screen directly from player");
            }
            else
            {
                Debug.LogError("Both GameManager and GameOverScreen are missing!");
            }
        }

        // Make player invisible instead of destroying immediately
        GetComponent<Renderer>().enabled = false;
        
        // Disable colliders
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = false;
        }
        
        // Destroy after a delay
        Destroy(gameObject, 2f);
    }
    
 }
    
