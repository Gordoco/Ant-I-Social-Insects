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

    [SerializeField] protected float swingMult = 3;

    [SerializeField] private float knockoutForce = 5000.0f;
    private float weight;

    public abstract EnemySpawner GetSpawnerType(GameObject beeType);

    public float GetSpawnWeight() { return weight; }

    public virtual FInteraction Interact(EInteractionType interactionType)
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.up * knockoutForce);
        return new FInteraction(EInteractionResult.None);
    }

    public virtual void Initialize(float spawnWeight)
    {
        weight = spawnWeight;
    }

    protected virtual void OnSpawned() =>
        Spawned?.Invoke(this, System.EventArgs.Empty);

    protected virtual void OnDead() =>
        Dead?.Invoke(this, System.EventArgs.Empty);

    protected virtual void OnInteraction(FInteraction interaction) =>
        Interaction?.Invoke(this, interaction);
}
