using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChaserBeeSpawner : EnemySpawner
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _player;

    // top left, bottom left, top right, bottom right
    [Tooltip("This should have 4 transforms")]

    [SerializeField] private Transform[] _spawnLocations;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            SpawnEnemy(_player);
        }
    }
    public override void SpawnEnemy(Transform player)
    {
        // choose left or right
        int horizontal = Random.Range(0, 2) == 1 ? 0 : 2;

        Vector2 start = _spawnLocations[horizontal].position;
        Vector2 end = _spawnLocations[horizontal + 1].position;

        Vector2 spawnPosition = Vector2.Lerp(start, end, Random.Range(0.0f, 1.0f));

        GameObject enemyObj = Instantiate(_enemyPrefab);
        ChaserBee bee = enemyObj.GetComponent<ChaserBee>();

        bee.PlayerPosition = player.position;
        bee.SpawnPosition = spawnPosition;
        bee.Initialize(0);
    }
}
