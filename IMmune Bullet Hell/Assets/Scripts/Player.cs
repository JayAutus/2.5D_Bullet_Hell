using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Boundary1
{
    public float xMin, xMax, zMin, zMax;
}

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    public Boundary1 boundary;

    public GameObject Shot;
    public Transform BulletSpawn;

    public Transform MultiShot;

    public bool hasShield = false;

    public bool isInvincible = false;

    private float originalFireRate;


    public Transform Shield;

    public InputAction fire;

    public float fireRate;

    private float nextFire;

    public void ActivateShield()
    {
        hasShield = true;
        Debug.Log("Shield activated!");
        // Optional: Add visual cue for shield :: will be worked on later
    }

    public void ModifyFireRate(float multiplier, float duration)
    {
        originalFireRate = fireRate;
        fireRate *= multiplier;
        Debug.Log("Fire rate increased!");
        StartCoroutine(ResetFireRate(duration));
    }

    IEnumerator ResetFireRate(float delay)
    {
        yield return new WaitForSeconds(delay);
        fireRate = originalFireRate;
        Debug.Log("Fire rate reset.");
    }

    public IEnumerator TemporaryInvincibility(float duration)
    {
        isInvincible = true;
        Debug.Log("Player is now invincible!");
        // Optional: Change player color to indicate invincibility
        yield return new WaitForSeconds(duration);
        isInvincible = false;
        Debug.Log("Invincibility ended.");
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fire = new InputAction("Attack", binding: "<Keyboard>/space"); // Or any key you prefer
        fire.Enable();
    }
    void Update()
    {
        if (fire.IsPressed() && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(Shot, BulletSpawn.position, BulletSpawn.rotation);
            foreach (Transform shotPoint in MultiShot)
            {
                Instantiate(Shot, shotPoint.position, shotPoint.rotation);
            }
        }

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        var fireAction = new InputAction("attack", binding: "<Keyboard>/Enter");

        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.linearVelocity = movement * speed;

        rigidbody.position = new Vector3
            (
                Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
                );



    }
}
