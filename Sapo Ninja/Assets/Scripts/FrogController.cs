using UnityEngine;
using UnityEngine.InputSystem;

public enum FrogState
{
    Grounded,
    Jumping,
    DoubleJumping,
    OnWall,
}

public class FrogController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float jumpSpeed = 6f;

    [SerializeField]
    private float doubleJumpSpeed = 5f;

    [SerializeField]
    private float maxJumpHoldTime = 0.2f;

    [SerializeField]
    private float wallJumpPushSpeed = 4f;

    [SerializeField]
    private float wallJumpControlLockTime = 0.15f;

    [SerializeField]
    private float wallSpeed = -0.75f;

    [Header("Detección del suelo y las paredes")]
    [SerializeField]
    private float castDistance = 0.05f;

    [SerializeField]
    private LayerMask mapLayer;

    [Header("Sonidos")]
    [SerializeField]
    private AudioClip jumpClip;

    [SerializeField]
    private AudioClip doubleJumpClip;

    [Header("Estado (solo para verlo en el inspector)")]
    [SerializeField]
    private FrogState state = FrogState.Grounded;

    const float MinNormal = 0.5f;

    static readonly int RunningParam = Animator.StringToHash("Running");
    static readonly int GroundedParam = Animator.StringToHash("Grounded");
    static readonly int DoubleJumpingParam = Animator.StringToHash("DoubleJumping");
    static readonly int OnWallParam = Animator.StringToHash("OnWall");
    static readonly int VelocityYParam = Animator.StringToHash("VelocityY");

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;

    private float movement = 0f;
    private bool facingRight = true;
    private bool jumpPressed = false;
    private bool grounded = true;
    private int wallSide = 0;
    private int lastWallSide = 0;
    private bool jumpHeld;
    private float jumpHoldTimer;
    private float wallJumpLockTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        ChangeState(state);
    }

    void Update()
    {
        movement = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            movement -= 1f;
        }

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            movement += 1f;
        }

        animator.SetBool(RunningParam, movement != 0);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpPressed = true;
        }

        jumpHeld = Keyboard.current.spaceKey.isPressed;
    }

    void FixedUpdate()
    {
        grounded = IsGrounded();

        if (!grounded && IsTouchingWall())
        {
            wallSide = facingRight ? 1 : -1;
        }
        else
        {
            wallSide = 0;
        }

        switch (state)
        {
            case FrogState.Grounded:
                UpdateGrounded();
                break;
            case FrogState.Jumping:
                UpdateJumping();
                break;
            case FrogState.DoubleJumping:
                UpdateDoubleJumping();
                break;
            case FrogState.OnWall:
                UpdateOnWall();
                break;
        }

        jumpPressed = false;
    }

    private void UpdateGrounded()
    {
        MoveHorizontally();
        if (jumpPressed)
        {
            Jump();
        }
        else if (!grounded)
        {
            ChangeState(FrogState.Jumping);
        }
    }

    private void UpdateJumping()
    {
        MoveHorizontally();
        SustainJump();
        animator.SetFloat(VelocityYParam, rb.linearVelocityY);

        if (jumpPressed)
        {
            DoubleJump();
        }
        else if (IsLanding())
        {
            ChangeState(FrogState.Grounded);
        }
        else if (!grounded && wallSide != 0 && CanGrabWall())
        {
            ChangeState(FrogState.OnWall);
        }
    }

    private void UpdateDoubleJumping()
    {
        MoveHorizontally();
        animator.SetFloat(VelocityYParam, rb.linearVelocityY);

        if (IsLanding())
        {
            ChangeState(FrogState.Grounded);
        }
        else if (!grounded && wallSide != 0 && CanGrabWall())
        {
            ChangeState(FrogState.OnWall);
        }
    }

    private void UpdateOnWall()
    {
        if (jumpPressed)
        {
            WallJump();
        }
        else if (IsLanding())
        {
            ChangeState(FrogState.Grounded);
        }
        else if (wallSide == 0)
        {
            ChangeState(FrogState.Jumping);
        }
        if (rb.linearVelocityY <= wallSpeed)
        {
            rb.linearVelocityY = wallSpeed;
        }
        MoveHorizontally();
        SustainJump();
    }

    private void ChangeState(FrogState newState)
    {
        state = newState;
        animator.SetBool(GroundedParam, state == FrogState.Grounded);
        animator.SetBool(OnWallParam, state == FrogState.OnWall);
        if (state == FrogState.DoubleJumping)
        {
            animator.SetTrigger(DoubleJumpingParam);
        }
        if (state == FrogState.Grounded)
        {
            lastWallSide = 0;
        }
        if (state == FrogState.OnWall)
        {
            lastWallSide = wallSide;
        }
        if (state == FrogState.Jumping)
        {
            animator.SetFloat(VelocityYParam, 0);
        }
    }

    private void MoveHorizontally()
    {
        if (!grounded && wallJumpLockTimer > 0)
        {
            wallJumpLockTimer -= Time.fixedDeltaTime;
            return;
        }
        else
        {
            wallJumpLockTimer = 0;
        }

        rb.linearVelocityX = movement * speed;
        if (movement > 0 && !facingRight || movement < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
    }

    private void Jump()
    {
        jumpHoldTimer = maxJumpHoldTime;
        rb.linearVelocityY = jumpSpeed;
        audioSource.PlayOneShot(jumpClip);
        ChangeState(FrogState.Jumping);
    }

    private void DoubleJump()
    {
        rb.linearVelocityY = doubleJumpSpeed;
        audioSource.PlayOneShot(doubleJumpClip);
        ChangeState(FrogState.DoubleJumping);
    }

    private void WallJump()
    {
        Flip();
        float direction = facingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(direction * wallJumpPushSpeed, wallJumpPushSpeed);
        jumpHoldTimer = 0f;
        wallJumpLockTimer = wallJumpControlLockTime;
        audioSource.PlayOneShot(doubleJumpClip);
        ChangeState(FrogState.DoubleJumping);
    }

    private void SustainJump()
    {
        if (jumpHeld && jumpHoldTimer > 0 && rb.linearVelocityY > 0)
        {
            rb.linearVelocityY = jumpSpeed;
            jumpHoldTimer -= Time.fixedDeltaTime;
        }
        else
        {
            jumpHoldTimer = 0f;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            castDistance,
            mapLayer
        );

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.normal.y >= MinNormal)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsLanding()
    {
        return grounded && rb.linearVelocityY <= 0;
    }

    private bool IsTouchingWall()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            facingRight ? Vector2.right : Vector2.left,
            castDistance,
            mapLayer
        );

        foreach (RaycastHit2D hit in hits)
        {
            if (Mathf.Abs(hit.normal.y) <= MinNormal)
            {
                return true;
            }
        }
        return false;
    }

    private bool CanGrabWall()
    {
        return wallSide != lastWallSide;
    }

    void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = grounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(
                boxCollider.bounds.center + Vector3.down * castDistance,
                boxCollider.bounds.size
            );

            Gizmos.color = wallSide != 0 ? Color.green : Color.red;
            Gizmos.DrawWireCube(
                boxCollider.bounds.center
                    + (facingRight ? Vector3.right : Vector3.left) * castDistance,
                boxCollider.bounds.size
            );
        }
    }
}
