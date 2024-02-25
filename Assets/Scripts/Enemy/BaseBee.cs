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
    [SerializeField] protected float smackMult = 4;

    [SerializeField] protected float knockoutForce = 5000.0f;
    [SerializeField] protected float stunTime;
    [SerializeField] private float weight = 1;
    [SerializeField] private float secondsUntilActive = 0;

    public abstract EnemySpawner GetSpawnerType(GameObject beeType);

    public float GetSpawnWeight() { return weight; }
    public float GetSpawnDelay() { return secondsUntilActive; }

    public virtual FInteraction Interact(EInteractionType interactionType)
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.up * knockoutForce);
        FInteraction result;
        switch (interactionType)
        {
            case EInteractionType.Stomp:
                result = new FInteraction(gameObject, EInteractionResult.Bounce, 1, stunTime);
                break;
            case EInteractionType.Swing:
                result = new FInteraction(gameObject, EInteractionResult.Bounce, swingMult, stunTime);
                break;
            case EInteractionType.Smack:
                result = new FInteraction(gameObject, EInteractionResult.Smack, smackMult, stunTime);
                break;
            default:
                result = new FInteraction(gameObject, EInteractionResult.Bounce, 0, stunTime);
                break;
        }
        OnInteraction(result);
        return result;
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
