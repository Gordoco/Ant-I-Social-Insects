using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    //EDITOR VARIABLES
    [SerializeField] private float jumpForce = 50.0f;

    [SerializeField] private float initialAngle = 45.0f;
    [SerializeField] private float initialForce = 100.0f;

    [SerializeField] private float strafeSpeed = 10.0f;
    [SerializeField] private float maxStrafeSpeed = 1.0f;
    [SerializeField] private float terminalYVelocity = -10.0f;

    [SerializeField] private float swordLength = 1.0f;
    [SerializeField] private float swingTime = 0.2f;

    [SerializeField] private AnimationClip attackAnimation;
    [SerializeField] private AnimationClip jumpAnimation;

    public event System.EventHandler SwingSword;
    public event System.EventHandler Bounce;
    public event System.EventHandler Die;
    //----------------

    private Rigidbody2D rb;
    private Animation anim;
    private bool bSwinging = false;
    private bool bHit = false;

    //UNITY MESSAGES------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Guarunteed due to requirements
        anim = GetComponent<Animation>(); // ^
        StartGame(); //Should move to some sort of menu later
    }

    float count = 0;
    // Update is called once per frame
    void Update()
    {
        InputHandling(); //For Debugging and testing without needing bees
        AddTerminalVelocity();
        if (!bSwinging) return;
        count += Time.deltaTime;
        if (count > swingTime)
        {
            bSwinging = false;
            count = 0;
            bHit = false;
        }
        else bHit = EvaluateHit().Length > 0;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<BaseBee>()) return;
        BaseBee beeClass = collision.gameObject.GetComponent<BaseBee>();
        if (bHit)
        {
            HandleInteraction(beeClass.Interact(EInteractionType.Swing));
            //Debug.Log("HEY YOU SLAPPED SOMEONE");
        }
        else HandleInteraction(beeClass.Interact(EInteractionType.Stomp));
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
        if (Input.GetButton("Jump") && !bSwinging)
        {
            /*//anim.Play(attackAnimation.name);
            GameObject[] hits = EvaluateHit();
            Debug.Log(hits.Length);
            for (int i = 0; i < hits.Length; i++)
            {
                BaseBee beeClass = hits[i].GetComponent<BaseBee>();
                if (!beeClass) continue;
                Debug.Log("SLAPPED");
                HandleInteraction(beeClass.Interact(EInteractionType.Swing));
            }*/
            bSwinging = true;
        }
    }

    private void AddTerminalVelocity()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, terminalYVelocity, -terminalYVelocity));
    }

    private GameObject[] EvaluateHit()
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        ContactFilter2D cf = new ContactFilter2D();
        int size = Physics2D.BoxCast(transform.position, GetComponent<Collider2D>().bounds.extents, 0, -transform.up, cf.NoFilter(), results, swordLength);
        GameObject[] arr = new GameObject[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = results[i].collider.gameObject;
        }
        return arr;
    }

    private void HandleInteraction(FInteraction interaction)
    {
        switch (interaction.result)
        {
            case EInteractionResult.Bounce:
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(jumpForce * interaction.bounceModifier * transform.up);
                //anim.Play(jumpAnimation.name);
                break;
            default:
                break;
        }
    }
}
