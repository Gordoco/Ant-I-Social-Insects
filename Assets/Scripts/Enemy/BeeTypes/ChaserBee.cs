using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChaserBee : BaseBee
{
    public Vector2 SpawnPosition { get; set; }
    public Vector2 PlayerPosition { get; set; }

    [SerializeField] private float Speed = 20f; // positive number; moving to the right

    private Rigidbody2D _rb;

    private void Awake()
    {
       _rb = GetComponent<Rigidbody2D>();
    }

    public override void Initialize(float minThreshold)
    {
        gameObject.SetActive(true);

        transform.position = SpawnPosition;
        _rb.velocity =
            new Vector2
            (
                Speed * Mathf.Sign(PlayerPosition.x - SpawnPosition.x),
                0
            );

        StartCoroutine(ChaserBeeAutoDeath());
        OnSpawned();
    }

    public override EnemySpawner GetSpawnerType(GameObject beeType)
    {
        return new ChaserBeeSpawner(20, beeType);
    }

    private IEnumerator ChaserBeeAutoDeath()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
        OnDead();
    }
}
