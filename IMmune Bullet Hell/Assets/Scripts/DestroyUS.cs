using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUS : MonoBehaviour{
    public int attackDamage = 1;
    GameObject PlayerObject;
    PlayerHealthTest playerHealth;

    void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = PlayerObject.GetComponent <PlayerHealthTest> ();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealth.TakeDamage (attackDamage);
            Destroy (gameObject);
        }
    }
}