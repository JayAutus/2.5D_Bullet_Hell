using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public int scoreValue = 5;
    public int attackDamage = 1;
    GameObject PlayerObject;
    EnemyHealth EplayerHealth;
    BulletHealth BulletHealth;

    void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Enemy");
        EplayerHealth = PlayerObject.GetComponent<EnemyHealth>();
        BulletHealth = PlayerObject.GetComponent<BulletHealth>();
    }
 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerObject)
        {
            EplayerHealth.TakeDamage(attackDamage);
            ScoreManager.score += scoreValue;
            Destroy(gameObject);
        }
    }
}