using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner : MonoBehaviour
{

    public abstract void SpawnEnemy(Transform player);
}
