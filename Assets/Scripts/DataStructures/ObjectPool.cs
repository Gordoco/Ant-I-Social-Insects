using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Implementation of an abstract object pool for efficient mass object management
 */
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;
    private GameObject[] objectPool;

    private void Awake()
    {
        objectPool = new GameObject[_poolSize];
        for (int i = 0; i < _poolSize; i++)
        {
            objectPool[i] = Instantiate(_prefab, Vector3.zero, Quaternion.identity) as GameObject;
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
