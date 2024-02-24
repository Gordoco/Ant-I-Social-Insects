using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.U2D;

public class ChaserBeeSpawner : EnemySpawner
{
    private Transform _player;

    public ChaserBeeSpawner(int poolSize, GameObject beeType) : base(poolSize, beeType)
    {}

    public override void SpawnEnemy(Transform player)
    {
        // choose left or right
        int horizontal = Random.Range(0, 2) == 1 ? 0 : 2;

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        PixelPerfectCamera pCamera = camera.GetComponent<PixelPerfectCamera>();
        Vector2[] spawnLocations = {
            new Vector2( 1.15f *  pCamera.refResolutionX/(pCamera.assetsPPU * 2),  pCamera.refResolutionY/(pCamera.assetsPPU * 2)),
            new Vector2( 1.15f *  pCamera.refResolutionX/(pCamera.assetsPPU * 2), -pCamera.refResolutionY/(pCamera.assetsPPU * 2)),
            new Vector2( 1.15f * -pCamera.refResolutionX/(pCamera.assetsPPU * 2),  pCamera.refResolutionY/(pCamera.assetsPPU * 2)),
            new Vector2( 1.15f * -pCamera.refResolutionX/(pCamera.assetsPPU * 2), -pCamera.refResolutionY/(pCamera.assetsPPU * 2)),
        };

        Vector2 start = spawnLocations[horizontal];
        Vector2 end = spawnLocations[horizontal + 1];

        Vector2 spawnPosition = Vector2.Lerp(start, end, Random.Range(0.0f, 1.0f));

        GameObject enemyObj = _enemyPool.GetObject();

        if (enemyObj == null) return;

        ChaserBee bee = enemyObj.GetComponent<ChaserBee>();

        bee.PlayerPosition = player.position;
        bee.SpawnPosition = spawnPosition;
        bee.Initialize(0);
    }
}
