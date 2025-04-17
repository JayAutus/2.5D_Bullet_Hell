using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static Transform[] waypoints;

    void Awake()
    {
        int count = transform.childCount;
        waypoints = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
