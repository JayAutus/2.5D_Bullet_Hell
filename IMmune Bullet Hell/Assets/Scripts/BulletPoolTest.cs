using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolTest : MonoBehaviour
{
    public static BulletPoolTest current;
    public GameObject ObjectForPool;
    public int PooledAmount = 20;
    public bool Growth = true;

    List<GameObject> ObjectsForPool;

    void Awake()
    {
        current = this;
    }
    void Start()
    {
        ObjectsForPool = new List<GameObject>();
        for (int i = 0; i < PooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(ObjectForPool);
            obj.SetActive(false);
            ObjectsForPool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < ObjectsForPool.Count; i++)
        {
            if (!ObjectsForPool [i].activeInHierarchy)
            {
                return ObjectsForPool[i];
            }
        }
        if (Growth)
        {
            GameObject obj = (GameObject)Instantiate (ObjectForPool);
            ObjectsForPool.Add(obj);
            return obj;
        }
        return null;
    }
}
