using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animation))]
public class PlayerController : MonoBehaviour
{
    //EDITOR VARIABLES
    public float jumpForce = 50.0f;
    public float initialAngle = 45.0f;
    public float initialForce = 100.0f;

    public AnimationClip[] animations;
    //----------------

    private Rigidbody2D rb;
    private Animation anim;

    //UNITY MESSAGES------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Guarunteed due to requirements
        anim = GetComponent<Animation>(); // ^
        StartGame(); //Should move to some sort of menu later
    }

    // Update is called once per frame
    void Update()
    {
        InputHandling(); //For Debugging and testing without needing bees
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<BaseBee>()) return;
        BaseBee beeClass = collision.gameObject.GetComponent<BaseBee>();
        HandleInteraction(beeClass.Interact(EInteractionType.Stomp));
    }
    //---------------------------------------------------------------------

    public void StartGame()
    {
        Vector2 angularVector = new Vector2(
            (Mathf.Cos(Mathf.Deg2Rad * initialAngle) * transform.right.x) + (Mathf.Sin(Mathf.Deg2Rad * initialAngle) * transform.right.y),
            (Mathf.Sin(Mathf.Deg2Rad * initialAngle) * transform.right.x) + (Mathf.Cos(Mathf.Deg2Rad * initialAngle) * transform.right.y)
            );
        angularVector.Normalize();
        rb.AddForce(angularVector * initialForce);
        Debug.Log(angularVector * initialForce);
    }

    private void InputHandling()
    {
        //TEST_HandleJump();
        HandleDownSwing();
    }

    private void HandleDownSwing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Sanity Check");
            anim.Play(animations[0].name);
            EvaluateHit();
        }
    }

    private bool EvaluateHit()
    {
        return false;
    }

    private void HandleInteraction(FInteraction interaction)
    {
        switch (interaction.result)
        {
            case EInteractionResult.Bounce:
                rb.AddForce(jumpForce * interaction.bounceModifier * transform.up);
                break;
            default:
                break;
        }
    }

    private void TEST_HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Sanity Check");
            rb.AddForce(jumpForce * transform.up);
        }
    }
}
