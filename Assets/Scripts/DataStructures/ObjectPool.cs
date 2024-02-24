using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Implementation of an abstract object pool for efficient mass object management
 */
public class ObjectPool
{
    private GameObject[] objectPool;

    public void initializePool(int poolSize, GameObject objectType)
    {
        objectPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            objectPool[i] = Object.Instantiate(objectType, Vector3.zero, Quaternion.identity);
            objectPool[i].SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        foreach(GameObject obj in objectPool)
        {
            if (!obj.activeSelf) return obj;
        }

        return null;
    }
}
