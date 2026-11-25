using UnityEngine;

public class RockHead : MonoBehaviour
{
    enum RockHeadState
    {
        Moving,
        Waiting,
    }

    [SerializeField]
    private Vector2 direction = Vector2.right;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float waitTime = 0.6f;

    [SerializeField]
    private LayerMask mapLayer;

    [SerializeField]
    private float castDistance = 0.1f;

    private const float MinNormal = 0.5f;

    static readonly int LeftHitParam = Animator.StringToHash("LeftHit");
    static readonly int RightHitParam = Animator.StringToHash("RightHit");
    static readonly int TopHitParam = Animator.StringToHash("TopHit");
    static readonly int BottomHitParam = Animator.StringToHash("BottomHit");

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private RockHeadState state = RockHeadState.Moving;

    private float waitTimer;
    private Vector2 targetDirection;
    private bool hasCollided = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        targetDirection = direction;
        ChangeState(state);
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case RockHeadState.Moving:
                UpdateMoving();
                break;
            case RockHeadState.Waiting:
                UpdateWaiting();
                break;
        }
    }

    private void UpdateMoving()
    {
        hasCollided = HasCollided();
        if (hasCollided)
        {
            TriggerHit();
            targetDirection *= -1;
            ChangeState(RockHeadState.Waiting);
        }
        else
        {
            Vector2 position = transform.position;
            rb.MovePosition(position + (targetDirection * speed * Time.fixedDeltaTime));
        }
    }

    private void UpdateWaiting()
    {
        waitTimer -= Time.fixedDeltaTime;
        if (waitTimer <= 0)
        {
            ChangeState(RockHeadState.Moving);
        }
    }

    private void ChangeState(RockHeadState newState)
    {
        state = newState;
        if (state == RockHeadState.Waiting)
        {
            waitTimer = waitTime;
        }
    }

    private bool HasCollided()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            targetDirection,
            castDistance,
            mapLayer
        );

        float normal = targetDirection.x != 0 ? hit.normal.x : hit.normal.y;
        return Mathf.Abs(normal) >= MinNormal;
    }

    private void TriggerHit()
    {
        if (targetDirection.x > 0)
        {
            animator.SetTrigger(RightHitParam);
        }
        else if (targetDirection.x < 0)
        {
            animator.SetTrigger(LeftHitParam);
        }
        else if (targetDirection.y < 0)
        {
            animator.SetTrigger(BottomHitParam);
        }
        else if (targetDirection.y > 0)
        {
            animator.SetTrigger(TopHitParam);
        }
    }

    void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = hasCollided ? Color.green : Color.red;
            Gizmos.DrawWireCube(
                boxCollider.bounds.center
                    + new Vector3(targetDirection.x, targetDirection.y) * castDistance,
                boxCollider.bounds.size
            );
        }
    }
}
