using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class JumperBeeSpawner : EnemySpawner
{
    public JumperBeeSpawner(int poolSize, GameObject beeType) : base(poolSize, beeType)
    { }


    public override void SpawnEnemy(Transform player)
    {

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        PixelPerfectCamera pCamera = camera.GetComponent<PixelPerfectCamera>();
        Vector2[] spawnLocations = {
            new Vector2( 1.15f *  pCamera.refResolutionX/(pCamera.assetsPPU * 2), -pCamera.refResolutionY/(pCamera.assetsPPU * 2)),
            new Vector2( 1.15f * -pCamera.refResolutionX/(pCamera.assetsPPU * 2), -pCamera.refResolutionY/(pCamera.assetsPPU * 2)),
        };

        Vector2 start = spawnLocations[0];
        Vector2 end = spawnLocations[1];

        Vector2 spawnPosition = Vector2.Lerp(start, end, Random.Range(0.0f, 1.0f));

        GameObject enemyObj = _enemyPool.GetObject();

        if (enemyObj == null) return;

        JumperBee bee = enemyObj.GetComponent<JumperBee>();

        bee.initialAngle = Random.Range(0, 2) == 1 ? 45 : 135;
        bee.transform.position = spawnPosition;
        bee.Initialize(0);
    }
}
