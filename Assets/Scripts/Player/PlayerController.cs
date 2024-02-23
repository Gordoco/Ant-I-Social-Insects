using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animation))]
public class PlayerController : MonoBehaviour
{
    //EDITOR VARIABLES
    [SerializeField] private float jumpForce = 50.0f;
    [SerializeField] private float initialAngle = 45.0f;
    [SerializeField] private float initialForce = 100.0f;

    [SerializeField] private float strafeSpeed = 10.0f;
    [SerializeField] private float maxStrafeSpeed = 1.0f;
    [SerializeField] private float terminalYVelocity = -10.0f;

    [SerializeField] private AnimationClip attackAnimation;
    [SerializeField] private AnimationClip jumpAnimation;
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
        AddTerminalVelocity();
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
        HandleHorizontalMovement();
        HandleDownSwing();
    }

    private void HandleHorizontalMovement()
    {
        float val = Input.GetAxis("Horizontal");
        rb.AddForce(transform.right * val * strafeSpeed);
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, 0, maxStrafeSpeed),
            rb.velocity.y
            );
    }

    private void HandleDownSwing()
    {
        if (Input.GetButton("Jump"))
        {
            Debug.Log("Sanity Check");
            anim.Play(attackAnimation.name);
            EvaluateHit();
        }
    }

    private void AddTerminalVelocity()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, terminalYVelocity, Mathf.Infinity));
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
                anim.Play(jumpAnimation.name);
                break;
            default:
                break;
        }
    }
}
