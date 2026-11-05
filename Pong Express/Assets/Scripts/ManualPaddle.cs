using UnityEngine;
using UnityEngine.InputSystem;

public class ManualPaddle : Paddle
{
    private float movement = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        movement = 0f;

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
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, movement * speed);
    }
}
