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

    public InputAction fire;

    public float fireRate;

    private float nextFire;

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
