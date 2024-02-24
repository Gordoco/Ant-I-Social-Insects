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

    [SerializeField] public GameObject poolTemplate;
    //---------------

    private IEnumerator Start()
    {
        EnemySpawner[] spawners = new EnemySpawner[1];
        for (int i = 0; i < 1; i++)
        {
            spawners[i] = beeTypes[i].GetComponent<BaseBee>().GetSpawnerType(beeTypes[i]);
        }
        while (true)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].SpawnEnemy(GameObject.FindGameObjectWithTag("Player").transform);
            }
            yield return new WaitForSeconds(0.1f);
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
