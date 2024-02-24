using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class BaseBee : MonoBehaviour
{
    public event System.EventHandler Spawned;
    public event System.EventHandler Dead;
    public event System.EventHandler<FInteraction> Interaction;

    [SerializeField] private float knockoutForce = 5000.0f;
    private float scoreThreshhold;

    public abstract EnemySpawner GetSpawnerType(GameObject beeType);

    public float GetScoreThreshold() { return scoreThreshhold; }

    public virtual FInteraction Interact(EInteractionType interactionType)
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.up * knockoutForce);
        return new FInteraction(EInteractionResult.None);
    }

    public virtual void Initialize(float minThreshold)
    {
        scoreThreshhold = minThreshold;
    }
}
