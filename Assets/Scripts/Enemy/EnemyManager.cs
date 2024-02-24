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

    [SerializeField] private float jumperRate = 0.2f;
    [SerializeField] private float specialRate = 0.7f;

    [SerializeField] public GameObject poolTemplate;
    //---------------

    private int score = 0;

    /*private IEnumerator Start()
    {
        while (true)
        {
            score++;
            SpawnSpecialBee();
            SpawnJumperBaseline();
            yield return new WaitForSeconds(0.3f);
        }
    }*/

    float jumperCount = 0;
    float specialCount = 0;
    private void Update()
    {
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
    }

    private void SpawnSpecialBee()
    {
        GameObject bee = GetBeeToSpawn();
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
            if (beeTypes[i].GetComponent<JumperBee>()) continue;
            sum += beeTypes[i].GetComponent<BaseBee>().GetSpawnWeight(); 
        }
        float rand = Random.Range(0.0f, sum);
        float count = 0;
        for (int i = 0; i < beeTypes.Length; i++)
        {
            if (beeTypes[i].GetComponent<JumperBee>()) continue;
            count += beeTypes[i].GetComponent<BaseBee>().GetSpawnWeight();
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
