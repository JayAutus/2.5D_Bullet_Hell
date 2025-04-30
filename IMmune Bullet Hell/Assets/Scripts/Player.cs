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

    public GameObject shieldPrefab; // assign in inspector
    private GameObject activeShield; // internal reference
    public float speed;

    private Rigidbody rb;

    public Boundary1 boundary;

    public GameObject Shot;
    public Transform BulletSpawn;

    public Transform MultiShot;

    

    bool hasShield = false;

    bool multishotBool = false;

    private float originalFireRate;

    

    public InputAction fire;

    public float fireRate;

    private float nextFire;

    public void ActivateShield()
        {
        if (activeShield != null) return; // prevent stacking

        hasShield = true;

        activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        activeShield.transform.SetParent(transform); // attach to player
        activeShield.transform.localPosition = Vector3.zero; // center on player

        Debug.Log("Shield activated!");
        }
    void OnDestroy()
    {
        Player player = GetComponentInParent<Player>();
        if (player != null)
        {
            player.hasShield = false;
        }
    }

    public void ModifyFireRate(float multiplier, float duration)
    {
        originalFireRate = fireRate;
        fireRate *= multiplier;
        multishotBool = true;
        Debug.Log("Fire rate increased!");
        StartCoroutine(ResetFireRate(duration));
    }

    IEnumerator ResetFireRate(float delay)
    {
        yield return new WaitForSeconds(delay);
        fireRate = originalFireRate;
        multishotBool = false;
        Debug.Log("Fire rate reset.");
    }


    private void Start(){
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
            if(multishotBool == true){
                foreach (Transform shotPoint in MultiShot)
                {
                    Instantiate(Shot, shotPoint.position, shotPoint.rotation);
                }
            }
        }

    }

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

        var fireAction = new InputAction("attack", binding: "<Keyboard>/Enter");

		var movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		var rigidbody = GetComponent<Rigidbody> ();
        rigidbody.linearVelocity = movement * speed;

        rigidbody.position = new Vector3
            (
                Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
                );
        
        
            
	}
    }

