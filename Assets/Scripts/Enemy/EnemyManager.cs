using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //EDITOR VARIABLES
    [SerializeField] private GameObject[] beeTypes;

    [SerializeField] private float minYSpawn;
    [SerializeField] private float maxYSpawn;
    [SerializeField] private float XSpawn;
    //---------------

    float count = 0;
    const float spawnTimer = 1;
    private void Update()
    {
        count += Time.deltaTime;
        if (count > spawnTimer)
        {
            count = 0;
            GameObject bee = Instantiate(GetBeeToSpawn(), new Vector3(XSpawn, Random.Range(minYSpawn, maxYSpawn), 0), transform.rotation);
            bee.GetComponent<BaseBee>().Initialize(0);
            if (bee == null) Debug.Log("ERROR: EnemyManager - Failed Bee Spawn");
        }
    }

    public GameObject GetBeeToSpawn()
    {
        float sum = 0;
        for (int i = 0; i < beeTypes.Length; i++) { sum += beeTypes[i].GetComponent<BaseBee>().GetScoreThreshold(); }
        float rand = Random.Range(0.0f, sum);
        float count = 0;
        for (int i = 0; i < beeTypes.Length; i++)
        {
            count += beeTypes[i].GetComponent<BaseBee>().GetScoreThreshold();
            if (count >= rand) return beeTypes[i];
        }
        Debug.Log("ERROR: EnemyManager - No Valid Bee Type Chosen");
        return null;
    }

    public GameObject[] GetBeesToSpawn(int num)
    {
        GameObject[] bees = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            bees[i] = GetBeeToSpawn();
        }
        return bees;
    }
}
