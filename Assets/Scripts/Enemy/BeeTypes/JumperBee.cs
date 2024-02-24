using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBee : BaseBee
{
    //EDITOR VARIABLES
    [SerializeField] public float initialAngle = 45.0f;
    [SerializeField] private float initialForce = 200.0f;
    //----------------

    public override void Initialize(float minThreshold)
    {
        base.Initialize(minThreshold);
        gameObject.SetActive(true);
        Vector2 angularVector = new Vector2(
            (Mathf.Cos(Mathf.Deg2Rad * initialAngle) * transform.right.x) + (Mathf.Sin(Mathf.Deg2Rad * initialAngle) * transform.right.y),
            (Mathf.Sin(Mathf.Deg2Rad * initialAngle) * transform.right.x) + (Mathf.Cos(Mathf.Deg2Rad * initialAngle) * transform.right.y)
            );
        angularVector.Normalize();
        GetComponent<Rigidbody2D>().AddForce(angularVector * initialForce);
    }

    private void Update()
    {
        if (transform.position.y < -8) gameObject.SetActive(false);
    }

    public override EnemySpawner GetSpawnerType(GameObject beeType)
    {
        return new JumperBeeSpawner(200, beeType);
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
                result = new FInteraction(EInteractionResult.Bounce, 0, swingMult);
                break;
            default:
                result = new FInteraction(EInteractionResult.Bounce);
                break;
        }
        return result;
    }
}
