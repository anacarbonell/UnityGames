using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 5f;

    [SerializeField]
    private AudioClip jumpClip;

    [SerializeField]
    private AudioClip hurtClip;

    private Rigidbody2D rb;
    private GameManager gameManager;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            !PauseManager.gameIsPaused
            && (
                Keyboard.current.spaceKey.wasPressedThisFrame
                || Mouse.current.leftButton.wasPressedThisFrame
            )
            && transform.position.y < 4
        )
        {
            audioSource.PlayOneShot(jumpClip);
            rb.linearVelocityY = jumpSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.EndGame();
            audioSource.PlayOneShot(hurtClip);

            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 0.6f);
        }
    }
}
