using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 5f;

    private Rigidbody2D rb;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            (
                Keyboard.current.spaceKey.wasPressedThisFrame
                || Mouse.current.leftButton.wasPressedThisFrame
            )
            && transform.position.y < 4
        )
        {
            rb.linearVelocityY = jumpSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.EndGame();
            Destroy(gameObject);
        }
    }
}
