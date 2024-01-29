using UnityEngine;
using UnityEngine.Events;

public class Character2DController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool airControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] float groundedRadius = .2f;                              // Radius of the overlap circle to determine if grounded
    private bool isGrounded;                                                  // Whether or not the player is grounded.
    private Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    [Header("Events")] [Space]
    public UnityEvent OnLandEvent;

    public bool IsGrounded => isGrounded;
    private bool IsFacingRight => transform.localScale.x > 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }


    private void FixedUpdate()
    {
        UpdateGroundedStatus();
    }


    public void Move(float velX, bool jump)
    {
        if (isGrounded || airControl)
        {
            Vector3 targetVelocity = new Vector2(velX * 10f, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

            if (IsFacingWrongSide(velX))
                Flip();
        }

        if (isGrounded && jump)
            Jump();
        
    }


    private void Flip()
    {
        Vector3 aux = transform.localScale;
        aux.x *= -1;
        transform.localScale = aux;
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);
    }


    public void Jump()
    {
        isGrounded = false;
        Vector2 auxVel = rb.velocity;
        auxVel.y = 0;
        rb.velocity = auxVel;
        rb.AddForce(new Vector2(0f, jumpForce));
    }


    private bool IsFacingWrongSide(float velX)
    {
        return velX > 0 && !IsFacingRight || velX < 0 && IsFacingRight;
    }


    private void UpdateGroundedStatus()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == gameObject)
                continue;

            isGrounded = true;
            if (!wasGrounded)
                OnLandEvent.Invoke();
        }
    }
}
