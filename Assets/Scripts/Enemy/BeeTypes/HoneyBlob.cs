using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyBlob : BaseBee
{
    //EDITOR VARIABLES
    [SerializeField] public float initialAngle = 45.0f;
    [SerializeField] private float initialForce = 600.0f;
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
        stunTime = 200f;
    }

    private void Update()
    {
        Vector2 v = GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (transform.position.y < -8) gameObject.SetActive(false);
    }

    public override EnemySpawner GetSpawnerType(GameObject beeType)
    {
        return new HoneyBlobSpawner(50, beeType);
    }

    public override FInteraction Interact(EInteractionType interactionType)
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.up * knockoutForce);
        FInteraction result;
        switch (interactionType)
        {
            case EInteractionType.Stomp:
            case EInteractionType.Swing:
            // only interaction with honey blob
            case EInteractionType.Smack:
                result = new FInteraction(gameObject, EInteractionResult.Kill, smackMult, stunTime, true, false);
                break;

            // otherwise do nothing
            default:
                result = new FInteraction(gameObject, EInteractionResult.None, 0, stunTime);
                break;
        }
        OnInteraction(result);
        return result;
    }
}
