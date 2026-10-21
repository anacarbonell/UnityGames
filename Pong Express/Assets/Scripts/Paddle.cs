using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public float minSize;
    public float mazSize;

    private Vector3 startPosition;
    private Vector3 startSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        startSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float movement = 0f;

        if (isPlayer1)
        {
            if (Keyboard.current[Key.W].isPressed)
            {
                movement++;
            }

            if (Keyboard.current[Key.S].isPressed)
            {
                movement--;
            }
        }
        else
        {
            if (Keyboard.current[Key.UpArrow].isPressed)
            {
                movement++;
            }

            if (Keyboard.current[Key.DownArrow].isPressed)
            {
                movement--;
            }
        }

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, movement * speed);
    }

    public void ResetPosition()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = startPosition;
    }

    public void ChangeSize(float amount)
    {
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Clamp(scale.y + amount, minSize, mazSize);
        transform.localScale = scale;
    }

    public void ResetSize()
    {
        transform.localScale = startSize;
    }
}
