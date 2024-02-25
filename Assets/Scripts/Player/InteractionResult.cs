

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
    public float stunTime;
    public GameObject other;
    public bool bHoney;
    public bool bExplode;

    public FInteraction(GameObject inOther,
                        EInteractionResult inResult,
                        float inBounceModifier = 1,
                        float inStunTime = 0,
                        bool inHoney = false,
                        bool inExplode = false)
    {
        other = inOther;
        result = inResult;
        stunTime = inStunTime;
        bounceModifier = inBounceModifier;
        bHoney = inHoney;
        bExplode = inExplode;
    }
}