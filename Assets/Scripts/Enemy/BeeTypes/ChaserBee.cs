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
        transform.position = SpawnPosition;
        _rb.velocity =
            new Vector2
            (
                Speed * Mathf.Sign(PlayerPosition.x - SpawnPosition.x),
                0
            );
    }
    public override FInteraction Interact(EInteractionType interaction)
    {
        FInteraction result;
        switch (interaction)
        {
            case EInteractionType.Stomp:
                result = new FInteraction(EInteractionResult.Bounce);
                break;
            default:
                result = new FInteraction(EInteractionResult.Bounce);
                break;
        }
        return result;
    }

}
