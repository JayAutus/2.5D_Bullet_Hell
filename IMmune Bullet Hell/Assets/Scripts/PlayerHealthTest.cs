using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthTest : MonoBehaviour
{
    public int startingHealth = 3;
    public int currentHealth;
    public Slider healthSlider;

    public Player player; // Link to the Player script

    void Awake()
    {
        currentHealth = startingHealth;
        player = GetComponent<Player>(); // or assign via Inspector if needed
    }

    public void TakeDamage(int amount)
    {
        if (player == null) return;

        if (player.isInvincible)
        {
            Debug.Log("Player is invincible. No damage taken.");
            return;
        }

        if (player.hasShield)
        {
            player.hasShield = false;
            Debug.Log("Shield absorbed the hit!");
            return;
        }

        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
