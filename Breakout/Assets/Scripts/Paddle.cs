using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float normalSpeed = 5f;

    [SerializeField]
    private float boostSpeed = 15f;

    [SerializeField]
    private AudioClip paddleHitClip;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public GameManager gameManager;

    private Rigidbody2D rb;
    private int movement = 0;
    private float speed;
    private bool speedIsBoosted;
    private float speedTimer;
    private float speedTime = 9f;
    private bool widthIsBoosted;
    private float widthTimer;
    private float widthTime = 9f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        movement = 0;
        if (Keyboard.current[Key.RightArrow].isPressed || Keyboard.current[Key.D].isPressed)
        {
            movement += 1;
        }

        if (Keyboard.current[Key.LeftArrow].isPressed || Keyboard.current[Key.A].isPressed)
        {
            movement -= 1;
        }

        if (speedIsBoosted)
        {
            speedTimer += Time.deltaTime;
            if (speedTimer > speedTime)
            {
                speedIsBoosted = false;
                speedTimer = 0;
                speed = normalSpeed;
            }
        }

        if (widthIsBoosted)
        {
            widthTimer += Time.deltaTime;
            if (widthTimer > widthTime)
            {
                widthIsBoosted = false;
                widthTimer = 0;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = movement * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            AudioManager.Instance.PlayClip(paddleHitClip);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject powerUp = collision.gameObject;
        if (powerUp.CompareTag("PowerUp_0"))
        {
            Destroy(powerUp);
            gameManager.AddBall(true);
        }
        else if (powerUp.CompareTag("PowerUp_1"))
        {
            Destroy(powerUp);
            speedIsBoosted = true;
            speed = boostSpeed;
            speedTimer = 0f;
        }
        else if (powerUp.CompareTag("PowerUp_2"))
        {
            Destroy(powerUp);
            widthIsBoosted = true;
            transform.localScale = new Vector3(1.25f, 1, 1);
            widthTimer = 0f;
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
            normalSpeed += 1f;
            boostSpeed += 2.5f;
            speed = normalSpeed;
        }
        else if (level == 2)
        {
            normalSpeed -= 1f;
            boostSpeed -= 2.5f;
            speed = normalSpeed;
        }
    }
}
