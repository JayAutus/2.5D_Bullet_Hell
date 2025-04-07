using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletHealth : MonoBehaviour
{

    public int startingHealth = 1;
    public int currentHealth;
    public Slider healthSlider;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
