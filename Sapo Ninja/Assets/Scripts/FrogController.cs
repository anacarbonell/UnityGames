using UnityEngine;
using UnityEngine.InputSystem;

public class FrogController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D rb;
    private Animator animator;

    static readonly int RunningParam = Animator.StringToHash("Running");

    private float movement = 0f;
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
    }

    void FixedUpdate()
    {
        MoveHorizontally();
    }

    private void MoveHorizontally()
    {
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
}
