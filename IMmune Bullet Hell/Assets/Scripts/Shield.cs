using UnityEngine;

public class Shield : MonoBehaviour
{
    public float lifetime = 3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject); // Destroy the incoming enemy bullet
            
        }
    }

    void Start(){
        Destroy(gameObject, lifetime);
    }
}