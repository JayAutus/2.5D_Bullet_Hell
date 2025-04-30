using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUS : MonoBehaviour{
    public int attackDamage = 1;
    GameObject PlayerObject;
    PlayerHealthTest playerHealth;

    void Start()
    {
        FindPlayer();
    }
    
    void FindPlayer()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObject != null)
        {
            playerHealth = PlayerObject.GetComponent<PlayerHealthTest>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // In case we didn't find the player on Start() or it changed
            if (playerHealth == null)
            {
                FindPlayer();
            }
            
            // Only damage if we have a valid reference
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
            
            Destroy(gameObject);
        }
    }
}