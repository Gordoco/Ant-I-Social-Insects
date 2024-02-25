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

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        PixelPerfectCamera pCamera = camera.GetComponent<PixelPerfectCamera>();
        float camX = camera.transform.position.x;
        float camY = camera.transform.position.y;
        Vector2[] spawnLocations = {
            new Vector2(camX + 1.15f *  pCamera.refResolutionX/(pCamera.assetsPPU * 2), camY + pCamera.refResolutionY/(pCamera.assetsPPU * 2)),
            new Vector2(camX + 1.15f *  pCamera.refResolutionX/(pCamera.assetsPPU * 2), camY + -pCamera.refResolutionY/(pCamera.assetsPPU * 2)),
            new Vector2(camX + 1.15f * -pCamera.refResolutionX/(pCamera.assetsPPU * 2), camY +  pCamera.refResolutionY/(pCamera.assetsPPU * 2)),
            new Vector2(camX + 1.15f * -pCamera.refResolutionX/(pCamera.assetsPPU * 2), camY + -pCamera.refResolutionY/(pCamera.assetsPPU * 2)),
        };

        int rand = Random.Range(0, 2);

        int side = rand == 1 ? 0 : 2;
        Vector2 start = spawnLocations[side];
        Vector2 end = spawnLocations[side + 1];

        Vector2 spawnPosition = Vector2.Lerp(start, end, Random.Range(0.0f, 1.0f));

        GameObject enemyObj = _enemyPool.GetObject();

        if (enemyObj == null) return;

        ChaserBee bee = enemyObj.GetComponent<ChaserBee>();

        bee.PlayerPosition = player.position;
        bee.transform.localScale *= rand == 1 ? -1 : 1;
        bee.transform.localScale = new Vector3(bee.transform.localScale.x, 1, 1);
        bee.SpawnPosition = spawnPosition;
        bee.Initialize(0);
    }
}
