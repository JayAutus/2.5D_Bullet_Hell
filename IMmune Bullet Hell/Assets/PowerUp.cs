using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Shield, FireRateBoost, Invincibility }
    public float fireRateMultiplier = 0.8f; // 20% faster
    public float invincibilityDuration = 3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerBullet"))
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            ApplyRandomEffect(player);
            Destroy(gameObject);
        }
    }

    void ApplyRandomEffect(Player player)
    {
        PowerUpType randomEffect = (PowerUpType)Random.Range(0, 3);

        switch (randomEffect)
        {
            case PowerUpType.Shield:
                player.ActivateShield();
                break;
            case PowerUpType.FireRateBoost:
                player.ModifyFireRate(fireRateMultiplier, 5f); // Boost lasts 5 seconds
                break;
            case PowerUpType.Invincibility:
                player.StartCoroutine(player.TemporaryInvincibility(invincibilityDuration));
                break;
        }
    }
}
