using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyBlobSpawner : EnemySpawner
{
    public HoneyBlobSpawner(int poolSize, GameObject beeType) : base(poolSize, beeType)
    { }

    public override void SpawnEnemy(Transform player)
    {
        // reference JumperBeeSpawner
        // TODO:
    }
}
