using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShots : MonoBehaviour {

    public GameObject shot;
    public Transform BulletSpawn;
    public float fireRate;
    private float nextFire;

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, BulletSpawn.position, BulletSpawn.rotation);
        }

    }
}
