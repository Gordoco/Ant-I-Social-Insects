using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //EDITOR VARIABLES
    [SerializeField] private GameObject[] beeTypes;
    private bool[] typeActive;

    [SerializeField] private float jumperRate = 0.2f;
    [SerializeField] private float specialRate = 0.7f;
    //---------------

    private void Start()
    {
        typeActive = new bool[beeTypes.Length];
        for (int i = 0; i < beeTypes.Length; i++) typeActive[i] = beeTypes[i].GetComponent<BaseBee>().GetSpawnDelay() == 0;
    }

    float delayCount = 0;
    float jumperCount = 0;
    float specialCount = 0;
    bool bAllDone = false;
    private void Update()
    {
        delayCount += Time.deltaTime;
        jumperCount += Time.deltaTime;
        specialCount += Time.deltaTime;

        if (jumperCount > jumperRate)
        {
            SpawnJumperBee();
            jumperCount = 0;
        }
        if (specialCount > specialRate)
        {
            SpawnSpecialBee();
            specialCount = 0;
        }

        if (!bAllDone) {
            bAllDone = true;
            for (int i = 0; i < beeTypes.Length; i++)
            {
                if (delayCount > beeTypes[i].GetComponent<BaseBee>().GetSpawnDelay()) typeActive[i] = true;
                if (!typeActive[i]) bAllDone = false;
            }
        }
    }

    private void SpawnSpecialBee()
    {
        GameObject bee = GetBeeToSpawn();
        if (!bee) return;
        EnemySpawner spawner = bee.GetComponent<BaseBee>().GetSpawnerType(bee);
        spawner.SpawnEnemy(GameObject.FindGameObjectWithTag("Player").transform);
    }

    private void SpawnJumperBee()
    {
        GameObject bee = null;
        for (int i = 0; i < beeTypes.Length; i++)
        {
            if (beeTypes[i].GetComponent<JumperBee>())
            {
                bee = beeTypes[i];
            }
        }
        if (!bee) return;
        EnemySpawner spawner = bee.GetComponent<BaseBee>().GetSpawnerType(bee);
        spawner.SpawnEnemy(GameObject.FindGameObjectWithTag("Player").transform);
    }

    public GameObject GetBeeToSpawn()
    {
        float sum = 0;
        for (int i = 0; i < beeTypes.Length; i++) 
        {
            if (beeTypes[i].GetComponent<JumperBee>() || !typeActive[i]) continue;

            sum += beeTypes[i].GetComponent<BaseBee>().GetSpawnWeight();
        }
        float rand = Random.Range(0.0f, sum);
        float count = 0;
        for (int i = 0; i < beeTypes.Length; i++)
        {
            if (beeTypes[i].GetComponent<JumperBee>() || !typeActive[i]) continue;

            count += beeTypes[i].GetComponent<BaseBee>().GetSpawnWeight();
            if (count >= rand) return beeTypes[i];
        }
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
