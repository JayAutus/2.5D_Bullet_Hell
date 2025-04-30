using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Shield, FireRateBoost, Invincibility }
    public float fireRateMultiplier = 0.8f;
    public float invincibilityDuration = 3f;
    public float lifeTime = 5f; // how long the power-up stays in the world

    void Start()
    {
        // Automatically destroy after lifeTime seconds
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            ApplyRandomEffect(player);
            Destroy(gameObject); // manually destroy after pickup
        }
    }

    void ApplyRandomEffect(Player player)
    {
        PowerUpType randomEffect = (PowerUpType)Random.Range(0, 2);

        switch (randomEffect)
        {
            case PowerUpType.Shield:
                player.ActivateShield();
                break;
            case PowerUpType.FireRateBoost:
                player.ModifyFireRate(fireRateMultiplier, 5f);
                break;
            
        }
    }
}