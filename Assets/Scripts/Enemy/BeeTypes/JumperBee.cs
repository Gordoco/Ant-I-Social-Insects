using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBee : BaseBee
{
    //EDITOR VARIABLES
    public float initialAngle = 135.0f;
    public float initialForce = 200.0f;
    //----------------

    public override void Initialize(float minThreshold)
    {
        base.Initialize(minThreshold);
        Vector2 angularVector = new Vector2(
            (Mathf.Cos(Mathf.Deg2Rad * initialAngle) * transform.right.x) + (Mathf.Sin(Mathf.Deg2Rad * initialAngle) * transform.right.y),
            (Mathf.Sin(Mathf.Deg2Rad * initialAngle) * transform.right.x) + (Mathf.Cos(Mathf.Deg2Rad * initialAngle) * transform.right.y)
            );
        angularVector.Normalize();
        GetComponent<Rigidbody2D>().AddForce(angularVector * initialForce);
        Debug.Log(angularVector * initialForce);
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
