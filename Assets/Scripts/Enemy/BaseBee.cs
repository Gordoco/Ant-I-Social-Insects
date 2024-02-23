using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class BaseBee : MonoBehaviour
{
    private float scoreThreshhold;

    public float GetScoreThreshold() { return scoreThreshhold; }

    abstract public FInteraction Interact(EInteractionType interactionType);

    virtual public void Initialize(float minThreshold)
    {
        scoreThreshhold = minThreshold;
    }
}
