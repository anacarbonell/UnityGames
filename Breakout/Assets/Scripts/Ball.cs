using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed = 5f;

    [SerializeField]
    private float heightOverPaddle = 0.3f;

    [SerializeField]
    private float minVerticalSpeed = 1.5f;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public Transform paddle;
    public GameManager gameManager;
    public bool launchAtStart;

    private Rigidbody2D rb;
    private bool launched = false;
    private bool launchRequested = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = paddle.position + Vector3.up * heightOverPaddle;
    }

    // Update is called once per frame
    void Update()
    {
        if (
            !PauseManager.Instance.GameIsPaused
            && Keyboard.current[Key.Space].wasPressedThisFrame
            && !launched
        )
        {
            launchRequested = true;
        }
    }

    void FixedUpdate()
    {
        if (!launched && (launchRequested || launchAtStart))
        {
            Launch();
        }
        else if (!launched)
        {
            rb.MovePosition(paddle.position + Vector3.up * heightOverPaddle);
        }
    }

    public void Launch()
    {
        launched = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        float horizontalSpeed = Random.Range(-initialSpeed, initialSpeed);
        rb.linearVelocity = new Vector2(horizontalSpeed, initialSpeed);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (Mathf.Abs(rb.linearVelocityY) < minVerticalSpeed)
        {
            if (rb.linearVelocityY > 0)
            {
                rb.linearVelocityY = minVerticalSpeed;
            }
            else
            {
                rb.linearVelocityY = -minVerticalSpeed;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            gameManager.DestroyBall();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle") && launched)
        {
            rb.linearVelocityX = (transform.position.x - collision.transform.position.x) * 2;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetLevel(int level)
    {
        if (level == 0)
        {
            initialSpeed -= 2f;
        }
        else if (level == 2)
        {
            initialSpeed += 2f;
        }
    }
}
