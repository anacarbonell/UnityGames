using UnityEngine;

public class Trampoline : MonoBehaviour
{
    enum TrampolineState
    {
        Idle,
        Jump,
    }

    [SerializeField]
    private float castDistance = 0.05f;

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private float jumpSpeed = 10f;

    [SerializeField]
    private float jumpTime = 0.4f;

    static readonly int JumpParam = Animator.StringToHash("Jump");

    private const float MinHeight = 0.3f;

    private Animator animator;
    private BoxCollider2D boxCollider;

    private TrampolineState state = TrampolineState.Idle;
    private float jumpTimer = 0.05f;
    private bool isPlayerJumping = false;
    private Rigidbody2D hitRb;

    void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        ChangeState(state);
    }

    void FixedUpdate()
    {
        isPlayerJumping = IsPlayerJumping();

        switch (state)
        {
            case TrampolineState.Idle:
                UpdateIdle();
                break;
            case TrampolineState.Jump:
                UpdateJump();
                break;
        }
    }

    private void UpdateIdle()
    {
        if (isPlayerJumping)
        {
            ChangeState(TrampolineState.Jump);
        }
    }

    private void UpdateJump()
    {
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.fixedDeltaTime;
            if (jumpTimer <= 0)
            {
                ChangeState(TrampolineState.Idle);
            }
        }
    }

    private bool IsPlayerJumping()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.up,
            castDistance,
            playerLayer
        );

        hitRb = hit.rigidbody;
        return hitRb != null
            && hit.transform.position.y > transform.position.y + MinHeight
            && hitRb.linearVelocityY < 0;
    }

    private void ChangeState(TrampolineState newState)
    {
        state = newState;
        if (state == TrampolineState.Jump)
        {
            animator.SetTrigger(JumpParam);
            jumpTimer = jumpTime;
            hitRb.linearVelocityY = jumpSpeed;
        }
    }

    void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = isPlayerJumping ? Color.green : Color.red;
            Gizmos.DrawWireCube(
                boxCollider.bounds.center + Vector3.up * castDistance,
                boxCollider.bounds.size
            );
        }
    }
}
