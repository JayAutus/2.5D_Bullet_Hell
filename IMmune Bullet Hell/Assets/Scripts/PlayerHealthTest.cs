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

    void Awake ()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage (int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        Instantiate(Hitsparks, transform.position, Quaternion.identity);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
        void Die()
    {
        if (ExplosionPrefab != null)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    
 }
    
