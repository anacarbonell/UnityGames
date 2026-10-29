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

    public Transform paddle;
    public GameManager gameManager;

    private Rigidbody2D rb;
    private bool launched = false;
    private bool launchRequested = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (!launched && Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            launchRequested = true;
        }
    }

    void FixedUpdate()
    {
        if (!launched && launchRequested)
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
            gameManager.LoseLive();
            Destroy(gameObject);
        }
    }
}
