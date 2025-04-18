using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;        // Set by spawner or inspector
    public float speed = 3f;
    public float reachThreshold = 0.2f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 0f;     // Degrees per second
    public Vector3 rotationAxis = Vector3.up; // Axis to rotate around (Y by default)

    private int currentIndex = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        MoveToNextWaypoint();
        RotateEnemy();
    }

    void MoveToNextWaypoint()
    {
        Transform target = waypoints[currentIndex];
        Vector3 direction = (target.position - transform.position).normalized;

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < reachThreshold)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length; // loop
        }
    }

    void RotateEnemy()
    {
        if (rotationSpeed != 0f)
        {
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }

    public void SetWaypoints(Transform[] path)
    {
        waypoints = path;
        currentIndex = 0;
    }
}
