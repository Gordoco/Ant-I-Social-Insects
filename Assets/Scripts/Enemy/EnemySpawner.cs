using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner
{

    protected ObjectPool _enemyPool;
    protected Transform[] _spawnLocations;

    public EnemySpawner(int poolSize, GameObject beeType) {
        _enemyPool = new ObjectPool();
        _enemyPool.initializePool(poolSize, beeType);
    }
    public abstract void SpawnEnemy(Transform player);
}
