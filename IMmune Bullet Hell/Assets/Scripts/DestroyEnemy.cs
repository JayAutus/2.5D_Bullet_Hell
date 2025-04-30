using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyEnemy : MonoBehaviour
{
    public int scoreValue = 5;
    public int attackDamage = 1;

    
    public GameObject hitSparksPrefab;

    private ScoreManager scoreManager;

    

    void OnTriggerEnter(Collider other)
    {
        // Try to find EnemyHealth on the object we hit
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        BulletHealth bulletHealth = other.GetComponent<BulletHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage);
            
            FindObjectOfType<ScoreManager>().AddScore(scoreValue);
            Instantiate(hitSparksPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy bullet
        }

        if (bulletHealth != null)
        {
            bulletHealth.TakeDamage(attackDamage);
            Destroy(gameObject);
        }
    }
}