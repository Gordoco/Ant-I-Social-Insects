public enum EInteractionResult
{
    Bounce,
    Damage,
    Kill
}

public struct FInteraction
{
    public EInteractionResult result;
    public float damage;
    public float bounceModifier;

    public FInteraction(EInteractionResult inResult, float inDamage = 0, float inBounceModifier = 1)
    {
        result = inResult;
        damage = inDamage;
        bounceModifier = inBounceModifier;
    }
}