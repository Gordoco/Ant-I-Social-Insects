using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ExploderBee : BaseBee
{

    [SerializeField] public float initialAngle = 45.0f;
    [SerializeField] private float initialForce = 500.0f;
    [SerializeField] private float explodeForceMult = 5f;
    [SerializeField] private SFX explodeSound;
    [SerializeField] private Sprite explodeSprite;

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
        return new ExploderBeeSpawner(50, beeType);
    }

    public override FInteraction Interact(EInteractionType interactionType)
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.up * knockoutForce);
        FInteraction result;
        switch (interactionType)
        {
            case EInteractionType.Stomp:
                result = new FInteraction(gameObject, EInteractionResult.Bounce);
                break;
            case EInteractionType.Swing:
                GetComponent<SpriteRenderer>().sprite = explodeSprite;
                GetComponent<SpriteRenderer>().color = Color.white;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                StartCoroutine(ExplodeTimer());
                SFXPlayer.PlaySFX(explodeSound);
                result = new FInteraction(gameObject, EInteractionResult.Kill, explodeForceMult, 200, false, true);
                break;
            // only interaction with honey blob
            case EInteractionType.Smack:
                result = new FInteraction(gameObject, EInteractionResult.Smack, smackMult, stunTime);
                break;

            // otherwise do nothing
            default:
                result = new FInteraction(gameObject, EInteractionResult.None, 0, stunTime);
                break;
        }
        OnInteraction(result);
        return result;
    }

    private IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
