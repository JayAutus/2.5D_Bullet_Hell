using System.Collections;
using UnityEngine;

public class ShootingBullets : MonoBehaviour
{
    public float speed;
    void Start ()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
	
		
	
}
