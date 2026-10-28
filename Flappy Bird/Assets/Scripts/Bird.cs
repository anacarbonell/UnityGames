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

    [SerializeField]
    private AudioClip explosionClip;

    private Rigidbody2D rb;
    private GameManager gameManager;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private int lifes = 2;
    private bool isInvencible = false;
    private float invencibleTimer = 0f;
    private float invencibleTime = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvencible)
        {
            invencibleTimer -= Time.deltaTime;
            if (invencibleTimer <= 0)
            {
                SetInvincibility(false);
            }
        }

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
        if (collision.gameObject.CompareTag("Pipe"))
        {
            if (isInvencible)
            {
                audioSource.PlayOneShot(explosionClip);
                Destroy(collision.gameObject);
                return;
            }

            TakeHit();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            TakeHit();
        }
    }

    private void TakeHit()
    {
        lifes--;
        audioSource.PlayOneShot(hurtClip);

        if (lifes == 0)
        {
            gameManager.EndGame();
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 0.6f);
        }
    }

    public void SetInvincibility(bool isInvencible)
    {
        this.isInvencible = isInvencible;
        spriteRenderer.color = isInvencible ? Color.cyan : Color.white;
        invencibleTimer = isInvencible ? invencibleTime : 0f;
    }
}
