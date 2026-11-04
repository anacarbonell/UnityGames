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

    public GameManager gameManager;

    private Rigidbody2D rb;
    private int movement = 0;
    private float speed;
    private bool speedIsBoosted;
    private float speedTimer;
    private float speedTime = 9f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        }
    }
}
