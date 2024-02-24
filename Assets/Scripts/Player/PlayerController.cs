using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

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
    [SerializeField] private float gravityScale = 1.0f;
    [SerializeField] private float fastFallRate = 1.0f;

    [SerializeField] private float swordLength = 1.0f; // AM: lol this hit box. how long is the sword actually?
    [SerializeField] private float swingTime = 0.66f; // AM: this

    [SerializeField] private float bounceTolerance = 0.3f;

    [SerializeField] private string gameOverScene = "Cody_Test";

    [SerializeField] private AnimationClip attackAnimation;
    [SerializeField] private AnimationClip jumpAnimation;

    public event System.EventHandler SwingSword;
    public event System.EventHandler Bounce;
    public event System.EventHandler Die;
    //----------------

    private Rigidbody2D rb;
    private Animator animator;
    private bool bSwinging = false;
    private bool bHit = false;
    private float forward = 0;
    private bool terminalVelOverride = false;
    private bool bDead = false;

    //UNITY MESSAGES------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Guarunteed due to requirements
        animator = GetComponent<Animator>(); // ^
        StartGame(); //Should move to some sort of menu later
    }

    float count = 0;
    // Update is called once per frame
    void Update()
    {
        InputHandling(Time.deltaTime); //For Debugging and testing without needing bees
        AddTerminalVelocity();
        animator.SetBool("rising",rb.velocity.y > 0); // AM: Sucks. Should be calced based on some position delta. mb acceleration if it's easily available.
        if (CheckForDead())
        {
            Die?.Invoke(this, System.EventArgs.Empty);
            bDead = true;
            SceneManager.LoadScene(gameOverScene);
        }

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
        if (!CheckCollisionDirection(collision.gameObject))
        {
            HandleInteraction(beeClass.Interact(EInteractionType.Smack));
        }
        else if (bHit)
        {
            HandleInteraction(beeClass.Interact(EInteractionType.Swing));
            //Debug.Log("HEY YOU SLAPPED SOMEONE");
        }
        else
        {
            HandleInteraction(beeClass.Interact(EInteractionType.Stomp));
        }
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

    private void InputHandling(float delta)
    {
        HandleHorizontalMovement(delta);
        HandleVerticalMovement(delta);
        HandleDownSwing();
    }

    private bool CheckCollisionDirection(GameObject other)
    {
        return transform.position.y > other.transform.position.y + bounceTolerance;
    }

    private void HandleHorizontalMovement(float delta)
    {
        float val = Input.GetAxis("Horizontal");

        if (rb.velocity.x >= maxStrafeSpeed && val > 0 || rb.velocity.x <= -maxStrafeSpeed && val < 0) return;
        if (rb.velocity.x >= -maxStrafeSpeed && rb.velocity.x <= maxStrafeSpeed && Mathf.Sign(val) != Mathf.Sign(forward) && val != 0) rb.velocity = new Vector2(0, rb.velocity.y);
        rb.AddForce(transform.right * val * strafeSpeed * delta, ForceMode2D.Impulse);

        forward = val;
    }

    private void HandleVerticalMovement(float delta)
    {
        float val = Input.GetAxis("Vertical");

        if (val >= 0)
        {
            terminalVelOverride = false;
            rb.gravityScale = gravityScale;
            return;
        }
        rb.gravityScale += delta * fastFallRate;
        terminalVelOverride = true;
    }

    private void HandleDownSwing()
    {
        if (Input.GetButton("Jump") && !bSwinging)
        {
            bSwinging = true;
            animator.SetTrigger("swing");

            SwingSword?.Invoke(this, System.EventArgs.Empty);
        }
    }

    private void AddTerminalVelocity()
    {
        if (!terminalVelOverride) rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, terminalYVelocity, -terminalYVelocity));
    }

    private bool CheckForDead()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        PixelPerfectCamera ppc = mainCamera.GetComponent<PixelPerfectCamera>();

        float height = (ppc.refResolutionY / (ppc.assetsPPU * 2));
        if (transform.position.y <= -(height+ 1.5)) return true;
        return false;
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
                rb.gravityScale = gravityScale;
                rb.AddForce(jumpForce * interaction.bounceModifier * transform.up);
                Bounce?.Invoke(this, System.EventArgs.Empty);
                //anim.Play(jumpAnimation.name);
                break;
            case EInteractionResult.Smack:
                rb.velocity = new Vector2(0, rb.velocity.y);
                int dirMult;
                if (interaction.other.transform.position.x - gameObject.transform.position.x > 0) dirMult = -1;
                else dirMult = 1;
                rb.AddForce(jumpForce * interaction.bounceModifier * transform.right * dirMult);
                break;
            default:
                break;
        }
    }
}
