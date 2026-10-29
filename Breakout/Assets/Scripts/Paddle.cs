using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D rb;
    private int movement = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

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
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = movement * speed;
    }
}
