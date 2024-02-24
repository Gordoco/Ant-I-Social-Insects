using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChaserBee : BaseBee
{
    public Vector2 SpawnPosition { get; set; }
    public Vector2 PlayerPosition { get; set; }

    const float Speed = 20f; // positive number; moving to the right

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
    }

    public override EnemySpawner GetSpawnerType(GameObject beeType)
    {
        return new ChaserBeeSpawner(20, beeType);
    }

    public override FInteraction Interact(EInteractionType interaction)
    {
        base.Interact(interaction);
        FInteraction result;
        switch (interaction)
        {
            case EInteractionType.Stomp:
                result = new FInteraction(EInteractionResult.Bounce);
                break;
            case EInteractionType.Swing:
                result = new FInteraction(EInteractionResult.Bounce, 0, 3);
                break;
            default:
                result = new FInteraction(EInteractionResult.Bounce);
                break;
        }
        return result;
    }

    private IEnumerator ChaserBeeAutoDeath()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
