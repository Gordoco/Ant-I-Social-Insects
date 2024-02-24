

using UnityEngine;

public enum EInteractionResult
{
    Bounce,
    Smack,
    Kill,
    None
}

public struct FInteraction
{
    public EInteractionResult result;
    public float bounceModifier;
    public GameObject other;

    public FInteraction(GameObject inOther, EInteractionResult inResult, float inBounceModifier = 1)
    {
        other = inOther;
        result = inResult;
        bounceModifier = inBounceModifier;
    }
}