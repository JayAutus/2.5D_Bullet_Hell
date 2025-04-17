using System.Collections;
using UnityEngine;

public class ShootingBullets : MonoBehaviour
{
    public float speed;
    void Start ()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
    }
	
		
	
}
