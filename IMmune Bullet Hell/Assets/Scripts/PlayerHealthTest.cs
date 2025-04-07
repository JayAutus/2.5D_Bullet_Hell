using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthTest : MonoBehaviour {

    public int startingHealth = 3;
    public int currentHealth;
    public Slider healthSlider;

    void Awake ()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage (int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
